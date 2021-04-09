using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TextConv
{
    public class ReplaceSkipRuleItem
    {
        public List<string> patterns = new List<string>();
        public bool isAnd = false;

        public bool isTrue(string content)
        {
            // no patterns, no skip check. 
            if (patterns.Count == 0) return false;

            // and 条件の場合、TrueForAllで判定
            if (isAnd)
            {
                //一つ条件が満たさない場合、不合格として、処理終了
                if (patterns.TrueForAll(r => Regex.IsMatch(content, r)))
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
                if (patterns.Exists(r => Regex.IsMatch(content, r)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
    public class ReplaceRuleItem
    {
        #region "Key Properties"
        public string pattern = string.Empty;
        public string replacement = string.Empty;
        public string repfile = string.Empty;
        public string function = string.Empty;
        public int nestLoopCount = 0;

        public List<ReplaceGroupItem> replacegroups = new List<ReplaceGroupItem>();
        
        public bool IgnoreCase = true;
        public bool Multiline = true;

        public string rangeFrom = string.Empty;
        public string rangeTo = string.Empty;
        public bool rangeSkip = false;

        public List<string> dicwords = new List<string>();

        public List<string> findInStrs = new List<string>();
        public bool findAnd = true;
        public bool findSkip = false;

        public List<string> excludeWords = new List<string>();
        public ReplaceSkipRuleItem skipMatch = null;
        public ReplaceSkipRuleItem skipInput = null;
        public ReplaceSkipRuleItem mustInput = null;
        public ReplaceRule parent = null;
        #endregion

        #region "private attributes"
        private Dictionary<LineMatch, LineMatch> rangeMatches = new Dictionary<LineMatch, LineMatch>();
        private Regex keyReg = null;
        private Regex valReg = null;
        private List<LineMatch> keys = new List<LineMatch>();
        private List<LineMatch> vals = new List<LineMatch>();
        
        private int lineNo = 0;
        //private int matchIndex = 0;
        //private List<string> origins = new List<string>();
        private bool commentMode
        {
            get
            {
                if (parent == null)
                {
                    return bool.Parse(Config.GetAppSettingValue("replace.comment.mode"));
                }
                return parent.commentMode;
            }
        }
        #endregion

        #region "public Get Properties"
        public RegexOptions RegOptions
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

        private List<string> repResults = new List<string>();
        public List<string> Results()
        {
            return repResults;
        }
        #endregion

        private string readRepfile(string filepath)
        {
            if (!File.Exists(filepath))
            {
                Console.WriteLine("★ERROR★ readRepfile: not found file:{0}", filepath);
                return string.Empty;
            }
            return File.ReadAllText(filepath);
        }

        #region "beforeReplace"
        private void beforeReplace(string[] lines)
        {
            //　範囲判定不要の場合、処理対象外
            if (!HasRangeCheck) return;
            keyReg = new Regex(rangeFrom, RegOptions);
            valReg = new Regex(rangeTo, RegOptions);

            rangeMatches.Clear();
            keys.Clear();
            vals.Clear();

            for (int i = 0; i < lines.Length; i++)
            {
                fillMatches(i + 1, lines[i]);
            }
            keys.Sort(CompareLineMatch);
            vals.Sort(CompareLineMatch);
        }

        //private void beforeReplace(string content)
        //{
        //    //　範囲判定不要の場合、処理対象外
        //    if (!HasRangeCheck) return;
        //    if (string.IsNullOrEmpty(content)) return;
        //    string[] lines = Regex.Split(content, @"[\r\n]");
        //    beforeReplace(lines);
        //    //keyReg = new Regex(rangeFrom, RegOptions);
        //    //valReg = new Regex(rangeTo, RegOptions);

        //    //rangeMatches.Clear();
        //    //keys.Clear();
        //    //vals.Clear();

        //    //fillMatches(0, content);
        //    //keys.Sort(CompareLineMatch);
        //    //vals.Sort(CompareLineMatch);
        //}
        private static int CompareLineMatch(LineMatch x, LineMatch y)
        {
            if (x.lineNo != y.lineNo)
            {
                return x.lineNo - y.lineNo;
            }
            return x.Match.Index - y.Match.Index;
        }
        private void fillMatches(int lineNo, string line)
        {
            MatchCollection m1 = keyReg.Matches(line);
            foreach (Match k in m1)
            {
                keys.Add(new LineMatch(lineNo, k));
            }
            MatchCollection m2 = valReg.Matches(line);
            foreach (Match v in m2)
            {
                vals.Add(new LineMatch(lineNo, v));
            }
        }

        private bool isInRange(int currentLineNo, Match m)
        {
            //今の行番に一番近い先頭行を取得
            LineMatch fromMatch = keys.FindLast(fm => (fm.lineNo == currentLineNo && fm.Match.Index <= m.Index) || fm.lineNo < currentLineNo);

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

        #endregion

        public string replaceText(string content)
        {
            // skip include content value
            if (skipInput != null)
            {
                // skip if match the pattern
                if (skipInput.isTrue(content))
                {
                    return content;
                }
            }
            // must required include content value
            if (mustInput != null)
            {
                // skip if not match the pattern.
                if (!mustInput.isTrue(content))
                {
                    return content;
                }
            }
            string[] lines = Regex.Split(content, @"[\r\n]");
            beforeReplace(lines);
            repResults.Clear();
            //==============================
            if (!string.IsNullOrEmpty(repfile))
            {
                string tmpValue = readRepfile(repfile);
                if (!string.IsNullOrEmpty(tmpValue)) 
                {
                    replacement = tmpValue;
                }
            }
            replacement = replacement.Replace("\\n", "\n");
            replacement = replacement.Replace("\\t", "\t");

            return replaceLines(lines);
            //Regex reg = new Regex(pattern, RegOptions);
            //int maxLoop = int.Parse(Config.GetAppSettingValue2("maxloop", "10"));

            //while (reg.IsMatch(content))
            //{
            //    matchIndex = 0;
            //    string newContent = reg.Replace(content, MatchReplacer);
            //    if (newContent.Equals(content))
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        content = newContent;
            //    }
            //    if (maxLoop < 0) break;
            //    maxLoop--;
            //}
            //==============================
            //return content;
        }
        private string replaceString(string content)
        {
            int loopCount = nestLoopCount;
            Regex reg = new Regex(pattern, RegOptions);
            while (reg.IsMatch(content))
            {
                //matchIndex = 0;
                string newContent = reg.Replace(content, MatchReplacer);
                if (newContent.Equals(content))
                {
                    break;
                }
                else
                {
                    content = newContent;
                }
                loopCount--;
                if (loopCount < 0) break;
            }
            return content;
        }
        private string replaceLines(string[] lines)
        {
            List<string> newLines = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                lineNo = i + 1;
                string nline = replaceString(line);
                if (!nline.Equals(line))
                {
                    if (commentMode)
                    {
                        string indent = UtilWxg.GetMatchGroup(line, @"^(\s+)(.+)", 1);
                        string oline = UtilWxg.GetMatchGroup(line, @"^(\s+)(.+)", 2);
                        string header = Config.GetAppSettingValue("replace.comment.header");
                        string marker = Config.GetAppSettingValue("replace.comment.marker");
                        string footer = Config.GetAppSettingValue("replace.comment.footer");

                        newLines.Add(string.Format("{0}{1}", indent, header));
                        newLines.Add(string.Format("{0}{1}{2}", indent, marker, oline));
                        newLines.Add(nline);
                        newLines.Add(string.Format("{0}{1}", indent, footer));
                    }
                    else
                    {
                        newLines.Add(nline);
                    }
                }
                else
                {
                    newLines.Add(nline);
                }
            }
            return string.Join("\n", newLines);
        }

        private string MatchReplacer(Match m)
        {
            //matchIndex++;
            
            // in range check
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

            // skip include match value
            if (skipMatch != null) 
            {
                if (skipMatch.isTrue(m.Value))
                {
                    return m.Value;
                }
            }

            string oldV = m.Value;
            string newV = m.Value;

            // function on groups 
            foreach (ReplaceGroupItem gi in this.replacegroups)
            {
                if (m.Groups.Count <= gi.groupindex) continue;

                foreach (Capture cpt in m.Groups[gi.groupindex].Captures)
                {
                    //SKIP対象の場合、スキップ
                    if (this.excludeWords.Contains(cpt.Value)) continue;

                    //dicwordsがあれば、優先利用
                    if (this.dicwords.Count > 0)
                    {
                        string v = dicwords.Find(x => x.Equals(cpt.Value, StringComparison.CurrentCultureIgnoreCase));
                        if (!string.IsNullOrEmpty(v) && !v.Equals(cpt.Value))
                        {
                            newV = Regex.Replace(newV, cpt.Value, v);
                            //優先利用の為、処理飛ばす
                            continue;
                        }
                    }
                    newV = Regex.Replace(newV, cpt.Value, OnFunction(cpt.Value, gi.function));
                }
            }

            // function on match
            if (!string.IsNullOrEmpty(this.function))
            {
                newV = OnFunction(newV, this.function);
            }
            if (!string.IsNullOrEmpty(replacement))
            {
                if(Regex.IsMatch(replacement, @"^NULL$", RegexOptions.IgnoreCase))
                {
                    replacement = string.Empty;
                }
                newV = Regex.Replace(m.Value, pattern, replacement, RegOptions);
            }
            
            if (!newV.Equals(oldV))
            {
                repResults.Add(string.Format("\t{0}\t{1}", oldV, newV));
            }
            return newV;
        }

        #region "private static methods"
        private static string OnFunction(string content, string functionName)
        {
            if (functionName.Equals("UCASE"))
            {
                return content.ToUpper();
            }

            if (functionName.Equals("LCASE"))
            {
                return content.ToLower();
            }
            return content;
        }

        #endregion
    }
    
}
