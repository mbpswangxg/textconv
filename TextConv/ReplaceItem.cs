using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TextConv
{
    public class ReplaceItem
    {
        public string cmdKey = string.Empty;
        public string pattern = string.Empty;
        public string replacement = string.Empty;
        public string repCmdKey = string.Empty;
        public string repGroup = string.Empty;

        public bool ignoreCase = true;
        public bool byLines = true;

        public string srcfile = string.Empty;
        public string repfile = string.Empty;

        public string rangeFrom = string.Empty;
        public string rangeTo = string.Empty;
        public bool skipedRange = false;
        public bool requiredRange = false;
        public Dictionary<LineMatch, LineMatch> rangeMatches = new Dictionary<LineMatch, LineMatch>();

        private Regex keyReg;
        private Regex valReg;
        private List<LineMatch> keys = new List<LineMatch>();
        private List<LineMatch> vals = new List<LineMatch>();

        public bool hasRangeCheck
        {
            get { return skipedRange || requiredRange; }
        }

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
                m = Regex.Match(words[i], @"skipedRange=(true|false)");
                if (m.Success)
                {
                    skipedRange = bool.Parse(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"requiredRange=(true|false)");
                if (m.Success)
                {
                    requiredRange = bool.Parse(m.Groups[1].Value);
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
                m = Regex.Match(words[i], @"srcfile=([^\t]+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    srcfile = m.Groups[1].Value;
                    continue;
                }
            }
            if(skipedRange && requiredRange)
            {
                throw new Exception("error: skipedRange=true or requiredRange=true, it's wrong for both true.");
            }
            if (hasRangeCheck)
            {
                if (string.IsNullOrEmpty(rangeFrom)
                || string.IsNullOrEmpty(rangeTo))
                {
                    
                }

                RegexOptions regOptions = RegexOptions.Multiline;
                if (ignoreCase)
                {
                    regOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                }

                keyReg = new Regex(rangeFrom, regOptions);
                valReg = new Regex(rangeTo, regOptions);
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
            if (string.IsNullOrEmpty(srcfile)) return false;
            return !Regex.IsMatch(filepath, srcfile, RegexOptions.IgnoreCase);
        }

        public void beforeReplace(string[] lines)
        {
            //　範囲判定不要の場合、処理対象外
            if (!hasRangeCheck) return;
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
            if (!hasRangeCheck) return;
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
            if (!hasRangeCheck) return;

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
        
    }
}
