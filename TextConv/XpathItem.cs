using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextConv
{
    public class XpathItem
    {
        public string name;
        public string nameXpath;

        public string eventText;
        public Dictionary<string, string> caseDescMap = new Dictionary<string, string>();
        public Dictionary<string, string> wordMap = new Dictionary<string, string>();
        public string caseDescFormat;

        public string textName;
        public SortedSet<string> textXpathSet = new SortedSet<string>();
 
        public XpathItem(string args)
        {
            string splitWords = Config.GetAppSettingValue2("splitwords", ";;;");
            string[] words = Regex.Split(args, splitWords);
            if (words.Length < 2)
            {
                string msg = string.Format("★★★args should be splited by tabkey, not spaces.★★★\nargs:【{0}】", args);
                throw new ArgumentException(msg);
            }

            Match m;

            name = words[0];
            m = Regex.Match(name, @"^(\w+)=(.+)");
            if (m.Success)
            {
                name = m.Groups[1].Value;
                nameXpath = m.Groups[2].Value;
            }
            
            for (int i = 1; i < words.Length; i++)
            {
                m = Regex.Match(words[i], @"^(text\w+)=(.+)");
                if (m.Success)
                {
                    textName = m.Groups[1].Value;
                    string textXpath = m.Groups[2].Value;
                    FileHelper.FillFromFile(textXpath, this.textXpathSet, string.Empty);
                }

                m = Regex.Match(words[i], @"eventText=(.+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    eventText = m.Groups[1].Value;
                    continue;
                }
                
                m = Regex.Match(words[i], @"caseDescFile=([^\t]+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    string caseDescFile = m.Groups[1].Value;
                    FileHelper.FillFromFile(caseDescFile, this.caseDescMap);
                    continue;
                }
                m = Regex.Match(words[i], @"caseDescFormat=(.+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    caseDescFormat = m.Groups[1].Value;
                    continue;
                }
                m = Regex.Match(words[i], @"worddictionary=([^\t]+)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    string wordmapfile = m.Groups[1].Value;
                    FileHelper.FillFromFile(wordmapfile, this.wordMap);
                    continue;
                }
            }
        }
        
    }
}
