using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TextConv
{
    public class ReplaceItem
    {
        #region "Key Properties"
        public string Name = string.Empty;
        public string Desc = string.Empty;

        public string pattern = string.Empty;
        public string replacement = string.Empty;
        public bool IgnoreCase = true;
        public bool Multiline = true;
        public string ExcapeText;
        public string inputContent = string.Empty;

        public string rangeFrom = string.Empty;
        public string rangeTo = string.Empty;
        public bool rangeSkip;

        public string destFolder = string.Empty;
        public string filefilter = string.Empty;
        public bool fileSkip;

        #endregion

        public string cmdKey = string.Empty;
        public string repCmdKey = string.Empty;

        public string repfile = string.Empty;
        public string currentfile = string.Empty;
        public List<string> repResults = new List<string>();

        private Dictionary<LineMatch, LineMatch> rangeMatches = new Dictionary<LineMatch, LineMatch>();
        private Regex keyReg = null;
        private Regex valReg = null;
        private List<LineMatch> keys = new List<LineMatch>();
        private List<LineMatch> vals = new List<LineMatch>();
        private List<string> iffindstrs = new List<string>();
        private List<string> ifnotfindstrs = new List<string>();
        private bool iffindand=true;

        private HashSet<string> excludeWords = new HashSet<string>();
        private HashSet<string> matchIndexes = new HashSet<string>();
        private bool skipMatchIndex = false;
        private List<string> dicwords = new List<string>();

        private int lineNo = 0;
        private int matchIndex = 0;

        public ReplaceItem() { }
        public ReplaceItem(string args) {
            string splitWords = Xmler.GetAppSettingValue("splitwords", ";;;");
            string[] words = Regex.Split(args, splitWords);

            Match m;

            pattern = words[0];
            replacement = words[1];
            m = Regex.Match(pattern, @"^(\w+)=(.+)");
            if (m.Success)
            {
                cmdKey = m.Groups[1].Value;
                pattern = m.Groups[2].Value;
            }

            m = Regex.Match(replacement, @"^(\w+)=(.+)");
            if (m.Success)
            {
                repCmdKey = m.Groups[1].Value;
                replacement = m.Groups[2].Value;
                
                if (repCmdKey.Equals("repfile"))
                {
                    replacement = readRepfile(replacement);
                }
            }
            // ３番目以降のパラメータはOptionで、必須ではない
            for (int i = 2; i < words.Length; i++)
            {
                m = Regex.Match(words[i], @"IgnoreCase=(true|false)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    IgnoreCase = bool.Parse(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"Multiline=(true|false)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    Multiline = !bool.Parse(m.Groups[1].Value);
                    continue;
                }
                
                m = Regex.Match(words[i], @"rangeFrom=([^\t]+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    rangeFrom = m.Groups[1].Value;
                    continue;
                }
                m = Regex.Match(words[i], @"rangeTo=([^\t]+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    rangeTo = m.Groups[1].Value;
                    continue;
                }
                m = Regex.Match(words[i], @"rangeSkip=(true|false)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    this.rangeSkip = bool.Parse(m.Groups[1].Value);
                    continue;
                }

                m = Regex.Match(words[i], @"filefilter=([^\t]+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    filefilter = m.Groups[1].Value;
                    continue;
                }
                m = Regex.Match(words[i], @"fileSkip=(true|false)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    this.fileSkip = bool.Parse(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"iffindstr=([^\t]+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    iffindstrs.Add(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"ifnotfindstr=([^\t]+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    ifnotfindstrs.Add(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"iffindand=(true|false)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    this.iffindand = bool.Parse(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"excludewords=([^\t]+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    FillSet(m.Groups[1].Value, this.excludeWords);
                    continue;
                }
                m = Regex.Match(words[i], @"excludefile=([^\t]+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    FillFromFile(m.Groups[1].Value, this.excludeWords);
                    continue;
                }
                m = Regex.Match(words[i], @"dicwordfile=([^\t]+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    FillFromFile(m.Groups[1].Value, this.dicwords);
                    continue;
                }
                m = Regex.Match(words[i], @"replaceIndexes=([^\t]+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    FillSet(m.Groups[1].Value, this.matchIndexes);
                    continue;
                }
                m = Regex.Match(words[i], @"skipMatchIndex=(true|false)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    this.skipMatchIndex = bool.Parse(m.Groups[1].Value);
                    continue;
                }
            }

        }
        private RegexOptions RegexOptions
        {
            get
            {
                RegexOptions regOptions = RegexOptions.None;
                if (IgnoreCase)
                {
                    regOptions = regOptions | RegexOptions.IgnoreCase;
                }
                if (Multiline)
                {
                    regOptions = regOptions | RegexOptions.Multiline;
                }

                return regOptions;
            }
        }
        private bool HasRangeCheck
        {
            get
            {
                if (string.IsNullOrEmpty(rangeFrom) || string.IsNullOrEmpty(rangeTo)) return false;
                return true;
            }
        }

        public void AppendToCommandFile(string path)
        {
            WriteCommandFile(path, true);
        }
        public void WriteCommandFile(string path, bool append)
        {
            List<string> lstParams = new List<string>();
            if (string.IsNullOrEmpty(this.cmdKey)) this.cmdKey = this.Name;
            if (string.IsNullOrEmpty(this.cmdKey)) this.cmdKey = "cmdKey";
            lstParams.Add(string.Format("{0}={1}", this.cmdKey, this.pattern));
            lstParams.Add(string.Format("repCmdKey={0}", this.replacement));
            if (HasRangeCheck)
            {
                lstParams.Add(string.Format("rangeSkip={0}", this.rangeSkip));
                lstParams.Add(string.Format("rangeFrom={0}", this.rangeFrom));
                lstParams.Add(string.Format("rangeTo={0}", this.rangeTo));
            }
            string cmd = string.Join("\t", lstParams) + "\n";
            if (append)
            {
                File.AppendAllText(path, cmd);
            }
            else
            {
                File.WriteAllText(path, cmd);
            }
        }
        private string readRepfile(string filepath)
        {
            if (!File.Exists(filepath))
            {
                Console.WriteLine("not found repfile:{0}", filepath);
                return filepath;
            }
            return File.ReadAllText(filepath);
        }

        private bool isSkipFile(string filepath)
        {
            if (string.IsNullOrEmpty(filefilter)) return false;
            
            if (Regex.IsMatch(filepath, filefilter, RegexOptions.IgnoreCase))
            {
                return this.fileSkip;
            }
            else
            {
                return !this.fileSkip;
            }
        }

        private void beforeReplace(string[] lines)
        {
            //　範囲判定不要の場合、処理対象外
            if (!HasRangeCheck) return;
            keyReg = new Regex(rangeFrom, RegexOptions);
            valReg = new Regex(rangeTo, RegexOptions);

            rangeMatches.Clear();
            keys.Clear();
            vals.Clear();

            for (int i = 0; i < lines.Length; i++)
            {
                fillMatches(i+1, lines[i]);
            }
            keys.Sort(CompareLineMatch);
            vals.Sort(CompareLineMatch);
        }

        private void beforeReplace (string content)
        {
            //　範囲判定不要の場合、処理対象外
            if (!HasRangeCheck) return;

            keyReg = new Regex(rangeFrom, RegexOptions);
            valReg = new Regex(rangeTo, RegexOptions);

            rangeMatches.Clear();
            keys.Clear();
            vals.Clear();

            fillMatches(0, content);
            keys.Sort(CompareLineMatch);
            vals.Sort(CompareLineMatch);
        }

        private bool mustExist(List<string> lines)
        {
            if (iffindstrs.Count > 0)
            {
                // and 条件の場合、TrueForAllで判定
                if (iffindand)
                {
                    
                    //一つ条件が満たさない場合、不合格として、処理終了
                    if (iffindstrs.TrueForAll(r => lines.Exists(l => Regex.IsMatch(l, r))))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else // or 条件の場合、Existsで判定
                {
                    //一つ条件も満たさない場合、不合格として、処理終了
                    if (iffindstrs.Exists(r => lines.Exists(l => Regex.IsMatch(l, r))))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
        }
        private bool mustNotExist(List<string> lines)
        {
            if (ifnotfindstrs.Count > 0)
            {
                // and 条件の場合、Existsで判定
                if (iffindand)
                {
                    //一つ条件が満たす場合、不合格として、処理終了
                    if (ifnotfindstrs.Exists(r => lines.Exists(l => Regex.IsMatch(l, r))))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else // or 条件の場合、TrueForAllで判定
                {
                    //全条件が満たす場合、不合格として、処理終了
                    if (ifnotfindstrs.TrueForAll(r => lines.Exists(l => Regex.IsMatch(l, r))))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }
        }
        private bool mustExist(string content)
        {
            if (iffindstrs.Count > 0)
            {
                // and 条件の場合、TrueForAllで判定
                if (iffindand)
                {
                    //一つ条件が満たさない場合、不合格として、処理終了
                    if (iffindstrs.TrueForAll(r => Regex.IsMatch(content, r)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else // or 条件の場合、Existsで判定
                {
                    //一つ条件も満たさない場合、不合格として、処理終了
                    if (iffindstrs.Exists(r => Regex.IsMatch(content, r)))
                    {
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
        }
        private bool mustNotExist(string content)
        {
            if (ifnotfindstrs.Count > 0)
            {
                // and 条件の場合、Existsで判定
                if (iffindand)
                {
                    //一つ条件が満たす場合、不合格として、処理終了
                    if (ifnotfindstrs.Exists(r => Regex.IsMatch(content, r)))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else // or 条件の場合、TrueForAllで判定
                {
                    //全条件が満たす場合、不合格として、処理終了
                    if (ifnotfindstrs.TrueForAll(r => Regex.IsMatch(content, r)))
                    {
                        return false;
                    }else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }
        }
        private bool isInRange(int currentLineNo, Match m)
        {
            //今の行番に一番近い先頭行を取得
            LineMatch fromMatch = keys.FindLast(fm => (fm.lineNo == currentLineNo && fm.Match.Index<= m.Index) || fm.lineNo < currentLineNo);
            
            if (fromMatch != null)
            {
                //先頭行対応する直後行を取得
                LineMatch toMatch = vals.Find(tm => (fromMatch.lineNo == tm.lineNo && fromMatch.Match.Index <= tm.Match.Index)
                                                    || fromMatch.lineNo < tm.lineNo);
                if (toMatch != null)
                {
                    if (toMatch.lineNo > currentLineNo)
                        return true;
                    else if (toMatch.lineNo == currentLineNo && toMatch.Match.Index >= m.Index) 
                        return true;
                }
            }
            return false;
        }

        private void fillMatches(int lineNo, string line)
        {
            MatchCollection m1 = keyReg.Matches(line);
            foreach(Match k in m1)
            {
                keys.Add(new LineMatch(lineNo, k));
            }
            MatchCollection m2 = valReg.Matches(line);
            foreach (Match v in m2)
            {
                vals.Add(new LineMatch(lineNo, v));
            }
        }

        private static int CompareLineMatch(LineMatch x, LineMatch y)
        {
            if (x.lineNo != y.lineNo)
            {
                return x.lineNo - y.lineNo;
            }
            return x.Match.Index - y.Match.Index;
        }

        public string replaceText(string content)
        {
            if (!mustExist(content) || !mustNotExist(content))
            {
                //見つかるべきものは見つからない場合、
                //見つからなくべきものが見つかった場合、
                //いずれも不合格として、処理終了
                return content;
            }

            beforeReplace(content);
            //==============================
            Regex reg = new Regex(pattern, RegexOptions);
            if (reg.IsMatch(content))
            {
                matchIndex = 0;
                content = reg.Replace(content, MatchReplacer);
            }
            //==============================
            return content;
        }

        public bool replaceLines(string[] lines)
        {
            List<string> tmpLines = new List<string>(lines);
            if (!mustExist(tmpLines) || !mustNotExist(tmpLines))
            {
                //見つかるべきものは見つからない場合、
                //見つからなくべきものが見つかった場合、
                //いずれも不合格として、処理終了
                return false;
            }

            beforeReplace(lines);
            //==============================
            string nline = string.Empty;
            bool hasChanged = false;
            bool innerChanged = false;
            Regex reg = new Regex(pattern, RegexOptions);
            for (int i = 0; i < lines.Length; i++)
            {
                nline = lines[i];
                lineNo = i + 1;
                while (reg.IsMatch(nline))
                {
                    innerChanged = false;
                    nline = reg.Replace(nline, MatchReplacer);
                    if (!nline.Equals(lines[i]))
                    {
                        lines[i] = nline;
                        innerChanged = true;
                    }
                    if (innerChanged)
                    {
                        hasChanged = true;
                    }
                    else
                    {
                        //変更対象外
                        break;
                    }
                }
            }

            return hasChanged;
        }

        public void ReplaceFile(string file)
        {
            if (!File.Exists(file)) return;
            if (isSkipFile(file)) return;
            currentfile = file;
            
            //if (!Multiline)
            //{
            //    string[] lines = File.ReadAllLines(file, FileHelper.Encoding);
            //    if (replaceLines(lines))
            //    {
            //        File.WriteAllLines(file, lines, FileHelper.Encoding);
            //    }
            //}
            //else
            {
                string content = File.ReadAllText(file, FileHelper.Encoding);
                string newContent = replaceText(content);
                if (!content.Equals(newContent))
                {
                    File.WriteAllText(file, newContent, FileHelper.Encoding);
                }
            }
        }
        private void FillFromFile(string file, ICollection<string> myset)
        {
            if (!File.Exists(file)) return;

            string[] lines = File.ReadAllLines(file, FileHelper.Encoding);
            foreach(string line in lines)
            {
                //空行を飛ばす
                if (Regex.IsMatch(line, @"^\s*$")) continue;

                //コメント行を飛ばす
                if (Regex.IsMatch(line, @"^(#|;|\-\-|\/\/)")) continue;
                FillSet(line, myset);
            }
        }
        
        private string MatchReplacer(Match m)
        {
            matchIndex++;
            string oldV = m.Value;
            string newV = m.Value;
            if (matchIndexes.Count > 0) 
            {
                if (matchIndexes.Contains(matchIndex.ToString()))
                {
                    if (skipMatchIndex)
                    {
                        return m.Value;
                    }
                }
                else
                {
                    if (!skipMatchIndex)
                    {
                        return m.Value;
                    }
                }
            }
            if (HasRangeCheck)
            {
                //範囲チェックあるの場合
                if (isInRange(lineNo, m))
                {
                    //範囲内のスキップ対象であれば、変更せずに戻る
                    if (rangeSkip) return m.Value;
                }
                else
                {
                    //範囲外のチェック対象であれば、
                    if (!rangeSkip) return m.Value;
                }
            }

            if (string.IsNullOrEmpty(repCmdKey)
                || repCmdKey.Equals("null") || repCmdKey.Equals("repCmdKey")
                || repCmdKey.Equals("repfile"))
            {
                replacement = replacement.Replace("\\n", "\n");
                replacement = replacement.Replace("\\t", "\t");
                
                newV = Regex.Replace(m.Value, pattern, replacement, RegexOptions);
            }
            else if (repCmdKey.EndsWith("CASE_GROUP"))
            {
                HashSet<string> repSet = new HashSet<string>();
                FillSet(replacement, repSet);
                for (int i = 0; i < m.Groups.Count; i++)
                {
                    if (!repSet.Contains(i.ToString())) continue;
                    
                    foreach (Capture cpt in m.Groups[i].Captures)
                    {
                        //SKIP対象の場合、スキップ
                        if (this.excludeWords.Contains(cpt.Value)) continue;

                        //dicwordsがあれば、優先利用
                        if (this.dicwords.Count>0)
                        {
                            string v = dicwords.Find(x => x.Equals(cpt.Value, StringComparison.CurrentCultureIgnoreCase));
                            if (string.IsNullOrEmpty(v)) continue;
                            if (!v.Equals(cpt.Value))
                            {
                                newV = Regex.Replace(newV, cpt.Value, v);
                            }
                            //優先利用の為、処理飛ばす
                            continue;
                        }

                        if (Regex.IsMatch(repCmdKey, "LCASE") && !cpt.Value.Equals(cpt.Value.ToLower()))
                        {
                            newV = Regex.Replace(newV, @"(\b+)(" + cpt.Value + @")(\b+)", "$1" + cpt.Value.ToLower() + "$3");
                        }
                        if (Regex.IsMatch(repCmdKey, "UCASE") && !cpt.Value.Equals(cpt.Value.ToUpper()))
                        {
                            newV = Regex.Replace(newV, @"(\b+)(" + cpt.Value + @")(\b+)", "$1" + cpt.Value.ToUpper() + "$3");
                        }
                    }
                }
            }
            else if (repCmdKey.Contains("DECODE_GROUP"))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("CASE ").Append(m.Groups[1].Value).Append(" ");
                sb.Append("WHEN ").Append(m.Groups[2].Value).Append(" ");
                Group grp = m.Groups[4];
                string sbWord = string.Empty;
                for (int i = 0; i < grp.Captures.Count; i++)
                {
                    if (i < grp.Captures.Count - 1)
                    {
                        if (i % 2 == 0)
                        {
                            sbWord = "THEN";
                        }
                        else
                        {
                            sbWord = "WHEN";
                        }
                        sb.Append(sbWord);
                        sb.Append(grp.Captures[i].Value).Append(" ");
                    }
                    else
                    {
                        if (sbWord.StartsWith("WHEN"))
                        {
                            sb.Append("THEN");
                        }
                        else
                        {
                            sb.Append("ELSE");
                        }
                        sb.Append(grp.Captures[i].Value).Append(" END");
                    }
                }
                for (int i = 5; i < m.Groups.Count; i++)
                {
                    sb.Append(m.Groups[i].Value);
                }
                newV = sb.ToString();
            }
            else
            {
                newV = Regex.Replace(m.Value, pattern, replacement, RegexOptions);
            }

            if (!newV.Equals(oldV))
            {
                repResults.Add(newV);
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", currentfile, pattern, oldV, newV);
            }
            return newV;
        }

        private void FillSet(string content, ICollection<string> destSet)
        {
            string[] words = Regex.Split(content, @"[\t,;]+");
            foreach(var w in words)
            {
                destSet.Add(w);
            }
        }
    }
}
