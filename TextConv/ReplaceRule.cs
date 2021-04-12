using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TextConv
{
    public class ReplaceRule
    {
        public string name = string.Empty;
        public List<ReplaceRuleItem> rules = new List<ReplaceRuleItem>();

        public string filefilter = string.Empty;
        public bool fileSkip = false;
        public bool commentMode = false;

        public string aftercheckpattern = string.Empty;

        private List<string> Results = new List<string>();
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
        public CommentConfigItem cconfig { get; private set; }
        public void Init()
        {
            foreach (ReplaceRuleItem item in rules)
            {
                item.parent = this;
            }
        }
        public void ReplaceFile(string file)
        {
            if (!File.Exists(file)) return;
            if (isSkipFile(file)) return;
            string ext = new FileInfo(file).Extension;
            cconfig = Config.GetCommentConfig(ext);

            string content = File.ReadAllText(file, Config.Encoding);
            Results.Clear();
            foreach (var rule in rules)
            {
                content = rule.replaceText(content);
                Results.AddRange(rule.Results());
            }

            if (Results.Count > 0)
            {
                File.WriteAllText(file, content, Config.Encoding);
                Console.WriteLine("{0}\n{1}", file, string.Join("\n", Results));
            }
            if (!string.IsNullOrEmpty(aftercheckpattern))
            {
                MatchCollection ms = Regex.Matches(content, aftercheckpattern);
                foreach (Match m in ms)
                {
                    Console.WriteLine("\t{0}\t{1}", aftercheckpattern, m.Value);
                }
            }
        }

        public void ReplaceFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath)) return;

            DirectoryInfo di = new DirectoryInfo(folderPath);
            foreach (FileInfo file in di.GetFiles())
            {
                ReplaceFile(file.FullName);
            }

            foreach (var sdi in di.GetDirectories())
            {
                ReplaceFolder(sdi.FullName);
            }
        }

    }
}
