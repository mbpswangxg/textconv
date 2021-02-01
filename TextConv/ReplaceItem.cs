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
        public bool ignoreCase = true;
        public bool multiLine = true;
        public string ExcapeText;


        public string rangeFrom = string.Empty;
        public string rangeTo = string.Empty;
        public bool rangeSkip;

        public string filefilter = string.Empty;
        public bool fileSkip;

        #endregion

        public string cmdKey = string.Empty;
        public string repCmdKey = string.Empty;

        public string repfile = string.Empty;
        public string currentfile = string.Empty;

        public Dictionary<LineMatch, LineMatch> rangeMatches = new Dictionary<LineMatch, LineMatch>();
        private Regex keyReg = null;
        private Regex valReg = null;
        private List<LineMatch> keys = new List<LineMatch>();
        private List<LineMatch> vals = new List<LineMatch>();
        
        private int lineNo = 0;
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
                    readRepfile(replacement);
                }
            }
            // ３番目以降のパラメータはOptionで、必須ではない
            for (int i = 2; i < words.Length; i++)
            {
                m = Regex.Match(words[i], @"ignoreCase=(true|false)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    ignoreCase = bool.Parse(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"multiline=(true|false)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    multiLine = !bool.Parse(m.Groups[1].Value);
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
                m = Regex.Match(words[i], @"rangeSkip=(true|false)");
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
                m = Regex.Match(words[i], @"fileSkip=(true|false)");
                if (m.Success)
                {
                    this.fileSkip = bool.Parse(m.Groups[1].Value);
                    continue;
                }
            }

            InitReplaceRule();
        }
        public RegexOptions RegexOptions
        {
            get
            {
                RegexOptions regOptions = RegexOptions.None;
                if (ignoreCase)
                {
                    regOptions = regOptions | RegexOptions.IgnoreCase;
                }
                if (multiLine)
                {
                    regOptions = regOptions | RegexOptions.Multiline;
                }

                return regOptions;
            }
        }
        public void InitReplaceRule()
        {
            if (HasRangeCheck)
            {
                keyReg = new Regex(rangeFrom, RegexOptions);
                valReg = new Regex(rangeTo, RegexOptions);
            }
        }

        private void readRepfile(string filepath)
        {
            if (!File.Exists(filepath))
            {
                Console.WriteLine("not found repfile:{0}", filepath);
                return;
            }
            replacement = File.ReadAllText(filepath);
        }

        public bool isSkipFile(string filepath)
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
        public bool HasRangeCheck
        {
            get {
                if (string.IsNullOrEmpty(rangeFrom) || string.IsNullOrEmpty(rangeTo)) return false;
                return true;
            }
        }
        public void beforeReplace(string[] lines)
        {
            //　範囲判定不要の場合、処理対象外
            if (!HasRangeCheck) return;
            
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

        public void beforeReplace (string content)
        {
            //　範囲判定不要の場合、処理対象外
            if (!HasRangeCheck) return;
            rangeMatches.Clear();
            keys.Clear();
            vals.Clear();

            fillMatches(0, content);
            keys.Sort(CompareLineMatch);
            vals.Sort(CompareLineMatch);
        }
        public bool isInRange(int currentLineNo, Match m)
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
            //==============================
            Regex reg = new Regex(pattern, RegexOptions);
            if (reg.IsMatch(content))
            {
                content = reg.Replace(content, MatchReplacer);
            }
            //==============================
            return content;
        }

        public bool replaceLines(string[] lines)
        {
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
            if (currentfile.Contains("search.js"))
            {
                Console.WriteLine(file);
            }
            string newContent = string.Empty;
            if (!multiLine)
            {
                string[] lines = File.ReadAllLines(file, FileHelper.Encoding);
                beforeReplace(lines);
                if (replaceLines(lines))
                {
                    FileInfo fi = new FileInfo(file);
                    File.WriteAllLines(file, lines, FileHelper.Encoding);
                }
                newContent = string.Join("\n", lines);
            }
            else
            {
                string content = File.ReadAllText(file, FileHelper.Encoding);
                beforeReplace(content);
                newContent = replaceText(content);
                if (!content.Equals(newContent))
                {
                    FileInfo fi = new FileInfo(file);
                    File.WriteAllText(file, newContent, FileHelper.Encoding);
                }
            }
        }

        private string MatchReplacer(Match m)
        {
            string oldV = m.Value;
            string newV = m.Value;

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
                || repCmdKey.Equals("null")
                || repCmdKey.Equals("repfile"))
            {
                newV = Regex.Replace(m.Value, pattern, replacement, RegexOptions);
            }
            else if (repCmdKey.EndsWith("CASE_GROUP"))
            {
                for (int i = 0; i < m.Groups.Count; i++)
                {
                    if (replacement.Contains(i.ToString()))
                    {
                        foreach (Capture cpt in m.Groups[i].Captures)
                        {
                            if (Regex.IsMatch(repCmdKey, "LCASE"))
                            {
                                newV = Regex.Replace(newV, cpt.Value, cpt.Value.ToLower());
                            }
                            if (Regex.IsMatch(repCmdKey, "UCASE"))
                            {
                                newV = Regex.Replace(newV, cpt.Value, cpt.Value.ToUpper());
                            }
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
                Console.WriteLine("repCmdKey:{0},replacement:{1}. " +
                    "注意: [=]が存在するreplacementであれば,repCmdKeyの命名が必須で,defaultでnull=replacementのformatにしてください",
                    repCmdKey, replacement);

                newV = Regex.Replace(m.Value, pattern, replacement, RegexOptions);
            }

            Console.WriteLine("{0}\t{1}\t{2}", currentfile, oldV, newV);
            return newV;
        }

    }
}
