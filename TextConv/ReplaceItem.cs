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
        public string excludeRangePattern = string.Empty;
        public bool byLines = true;
        public string srcfile = string.Empty;
        public string repfile = string.Empty;

        public ReplaceItem() { }
        public ReplaceItem(string[] owords) {
            Match m;
            List<string> origins = new List<string>(owords);
            List<string> newwords = origins.FindAll(r => !string.IsNullOrEmpty(r));
            string[] words = newwords.ToArray();
            
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

                m = Regex.Match(words[i], @"ignoreCase=(\w+)");
                if (m.Success)
                {
                    ignoreCase = bool.Parse(m.Groups[1].Value);
                    continue;
                }
                m = Regex.Match(words[i], @"bylines=(.+)",RegexOptions.IgnoreCase);
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
        public bool SkipFile(string filepath)
        {
            if (string.IsNullOrEmpty(srcfile)) return false;
            return !Regex.IsMatch(filepath, srcfile, RegexOptions.IgnoreCase);
        }
    }
}
