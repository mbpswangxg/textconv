using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TextConv
{
    public enum ReplaceChecks
    {
        none = 0,
        skip,
        replace
    }

    public class ReplaceItem
    {
        public string cmdKey = string.Empty;
        public string pattern = string.Empty;
        public string replacement = string.Empty;
        public string repCmdKey = string.Empty;
        public string repGroup = string.Empty;

        public RegexOptions regOptions;
        public bool ignoreCase = true;
        public bool byLines = true;

        /*public string inputText = string.Empty;
        public string inputFile = string.Empty;
        public string inputFolder = string.Empty;*/

        public string filefilter = string.Empty;
        public string repfile = string.Empty;

        public string rangeFrom = string.Empty;
        public string rangeTo = string.Empty;
        
        public Dictionary<LineMatch, LineMatch> rangeMatches = new Dictionary<LineMatch, LineMatch>();

        private Regex keyReg = null;
        private Regex valReg = null;
        private List<LineMatch> keys = new List<LineMatch>();
        private List<LineMatch> vals = new List<LineMatch>();

        public ReplaceChecks RangeCheck = ReplaceChecks.none;
        public ReplaceChecks FileFilterCheck = ReplaceChecks.none;
        private int lineNo = 0;
        public ReplaceItem() { }
        public ReplaceItem(string[] words) {
            Match m;

            pattern = words[0];
            replacement = words[1];
            m = Regex.Match(pattern, @"^([^=]+)=(.+)");
            if (m.Success)
            {
                cmdKey = m.Groups[1].Value;
                pattern = m.Groups[2].Value;
            }

            m = Regex.Match(replacement, @"^(^\w+)=(.+)");
            if (m.Success)
            {
                repCmdKey = m.Groups[1].Value;
                repGroup = m.Groups[2].Value;
                replacement = repGroup;
                repfile = repGroup;
                if (repCmdKey.Equals("repfile"))
                {
                    readRepfile(repGroup);
                }
            }

            for (int i = 2; i < words.Length; i++)
            {
                m = Regex.Match(words[i], @"ignoreCase=(true|false)");
                if (m.Success)
                {
                    ignoreCase = bool.Parse(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"bylines=(true|false)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    byLines = bool.Parse(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"bytext=(true|false)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    byLines = !bool.Parse(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"rangeCheck=([0-2])");
                if (m.Success)
                {
                    int rc = int.Parse(m.Groups[1].Value);
                    this.RangeCheck =(ReplaceChecks) Enum.Parse(typeof(ReplaceChecks), m.Groups[1].Value); 
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
                m = Regex.Match(words[i], @"filefilter=([^\t]+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    filefilter = m.Groups[1].Value;
                    continue;
                }
            }

            InitReplaceRule();
        }
        public void InitReplaceRule()
        {
            if (RangeCheck != ReplaceChecks.none)
            {
                if (string.IsNullOrEmpty(rangeFrom) || string.IsNullOrEmpty(rangeTo))
                {
                    throw new Exception("error: rangeFrom and rangeTo are required.");
                }

                regOptions = RegexOptions.Multiline;
                if (ignoreCase)
                {
                    regOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                }

                keyReg = new Regex(rangeFrom, regOptions);
                valReg = new Regex(rangeTo, regOptions);
            }
            if (FileFilterCheck != ReplaceChecks.none)
            {
                if (string.IsNullOrEmpty(rangeFrom) || string.IsNullOrEmpty(rangeTo))
                {
                    throw new Exception("error: rangeFrom and rangeTo are required.");
                }
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
            return !Regex.IsMatch(filepath, filefilter, RegexOptions.IgnoreCase);
        }

        public void beforeReplace(string[] lines)
        {
            //　範囲判定不要の場合、処理対象外
            if (RangeCheck == ReplaceChecks.none) return;
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
            if (RangeCheck == ReplaceChecks.none) return;
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
            LineMatch fromMatch = keys.FindLast(fm => fm.lineNo <= currentLineNo);
            
            if (fromMatch != null)
            {
                //先頭行対応する直後行を取得
                LineMatch toMatch = vals.Find(tm => fromMatch.lineNo <= tm.lineNo);
                if (toMatch != null)
                {
                    //今の行番に一番近い後ろ行を取得
                    LineMatch toMatch2 = vals.Find(tm => currentLineNo <= tm.lineNo);
                    return toMatch.Equals(toMatch2);
                }
            }
            return false;
        }

        private void fillMatches(int lineNo, string line)
        {
            //　範囲判定不要の場合、処理対象外
            if (RangeCheck == ReplaceChecks.none) return;

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
            Regex reg = new Regex(pattern, regOptions);
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
            Regex reg = new Regex(pattern, regOptions);
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

        private string MatchReplacer(Match m)
        {
            string oldV = m.Value;
            string newV = m.Value;

            if (RangeCheck != ReplaceChecks.none)
            {
                //範囲チェックあるの場合
                if (isInRange(lineNo, m))
                {
                    //範囲内のスキップ対象であれば、変更せずに戻る
                    if (RangeCheck == ReplaceChecks.skip) return m.Value;
                }
                else
                {
                    //範囲外のチェック対象であれば、変更せずに戻る
                    if (RangeCheck == ReplaceChecks.replace) return m.Value;
                }
            }

            if (string.IsNullOrEmpty(repCmdKey)
                || repCmdKey.Equals("null")
                || repCmdKey.Equals("repfile"))
            {
                newV = Regex.Replace(m.Value, pattern, replacement, regOptions);
            }
            else if (repCmdKey.Contains("UCASE_GROUP"))
            {
                for (int i = 0; i < m.Groups.Count; i++)
                {
                    if (repGroup.Contains(i.ToString()))
                    {
                        foreach (Capture cpt in m.Groups[i].Captures)
                        {
                            newV = Regex.Replace(newV, cpt.Value, cpt.Value.ToUpper());
                        }
                    }
                }
            }
            else if (repCmdKey.Contains("LCASE_GROUP"))
            {
                for (int i = 0; i < m.Groups.Count; i++)
                {
                    if (repGroup.Contains(i.ToString()))
                    {
                        foreach (Capture cpt in m.Groups[i].Captures)
                        {
                            newV = Regex.Replace(newV, cpt.Value, cpt.Value.ToLower());
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
                for (int i = 0; i < grp.Captures.Count; i++)
                {
                    if (i < grp.Captures.Count - 1)
                    {
                        if (i % 2 == 0)
                        {
                            sb.Append("THEN ");
                        }
                        else
                        {
                            sb.Append("WHEN ");
                        }
                        sb.Append(grp.Captures[i].Value).Append(" ");
                    }
                    else
                    {
                        sb.Append("ELSE ").Append(grp.Captures[i].Value).Append(" END ");
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
                Console.WriteLine("repCmdKey:{0},repGroup:{1},replacement:{2}. " +
                    "注意: [=]が存在するreplacementであれば,repCmdKeyの命名が必須で,defaultでnull=replacementのformatにしてください",
                    repCmdKey, repGroup, replacement);

                newV = Regex.Replace(m.Value, pattern, replacement, regOptions);
            }

            //Console.WriteLine("{0}\t{1}\t{2}", currentfile, oldV, newV);
            return newV;
        }

    }
}
