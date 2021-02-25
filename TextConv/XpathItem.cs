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
        public string caseDescFormat;

        public string textName;
        public string textXpath;
        public string text;


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
            text = words[1];
            m = Regex.Match(text, @"^(\w+)=(.+)");
            if (m.Success)
            {
                textName = m.Groups[1].Value;
                textXpath = m.Groups[2].Value;
            }

            for (int i = 2; i < words.Length; i++)
            {
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
            }
        }
        
    }
}
