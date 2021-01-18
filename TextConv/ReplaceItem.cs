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

        public bool hasRangeCheck
        {
            get { return skipedRange || requiredRange; }
        }

        public ReplaceItem() { }
        public ReplaceItem(string[] owords) {
            Match m;
            List<string> origins = new List<string>(owords);
            List<string> words = origins.FindAll(r => !string.IsNullOrEmpty(r));
            if (words.Count < 2)
            {
                Console.WriteLine("error reg settings line: [{0}]", string.Join(",", words));
                return;
            }

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

            for (int i = 2; i < words.Count; i++)
            {
                m = Regex.Match(words[i], @"ignoreCase=(\w+)");
                if (m.Success)
                {
                    ignoreCase = bool.Parse(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"bylines=(.+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    byLines = bool.Parse(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"bytext=(.+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    byLines = !bool.Parse(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"srcfile=(.+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    srcfile = m.Groups[1].Value;
                    continue;
                }
                m = Regex.Match(words[i], @"skipedRange=(\w+)");
                if (m.Success)
                {
                    skipedRange = bool.Parse(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"requiredRange=(\w+)");
                if (m.Success)
                {
                    requiredRange = bool.Parse(m.Groups[1].Value);
                    continue;
                }

                m = Regex.Match(words[i], @"rangeFrom=(.+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    rangeFrom = m.Groups[1].Value;
                    continue;
                }
                m = Regex.Match(words[i], @"rangeTo=(.+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    rangeTo = m.Groups[1].Value;
                    continue;
                }
            }
            if (hasRangeCheck)
            {
                if (string.IsNullOrEmpty(rangeFrom)
                || string.IsNullOrEmpty(rangeTo))
                {
                    throw new Exception("when skipedRange/requiredRange=true, rangeFrom and rangeTo are required in repfile.txt.");
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
            if (string.IsNullOrEmpty(srcfile)) return false;
            return !Regex.IsMatch(filepath, srcfile, RegexOptions.IgnoreCase);
        }

        public void beforeReplace(string[] lines)
        {
            //　範囲判定不要の場合、処理対象外
            if (!hasRangeCheck) return;
            for (int i = 0; i < lines.Length; i++)
            {
                beforeReplace(i, lines[i]);
            }
        }
        public void beforeReplace (string content)
        {
            beforeReplace(0, content);
        }
        private void beforeReplace(int lineNo, string line)
        {
            //　範囲判定不要の場合、処理対象外
            if (!hasRangeCheck) return;

            RegexOptions regOptions = RegexOptions.Multiline;
            if (ignoreCase) 
            {
                regOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
            }
            
            MatchCollection ms1 = Regex.Matches(line, rangeFrom, regOptions);
            MatchCollection ms2 = Regex.Matches(line, rangeTo, regOptions);

            foreach (Match m1 in ms1)
            {
                foreach (Match m2 in ms2)
                {
                    if (m1.Index > m2.Index)
                    {
                        continue;
                    }
                    rangeMatches.Add(new LineMatch(lineNo, m1), new LineMatch(lineNo, m2));
                    break;
                }
            }
        }

    }
}
