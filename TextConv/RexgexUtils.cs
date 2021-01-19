using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace TextConv
{
    public class RexgexUtils
    {
        private string cmd;
        private List<ReplaceItem> rules;
        private string currentfile = string.Empty;
        private int currentLineNo = 0;
        private ReplaceItem currentRule = null;
        private RegexOptions regOptions = RegexOptions.IgnoreCase | RegexOptions.Multiline;
        private string filefilter = string.Empty;
        
        public List<string> msgs;

        public RexgexUtils(string cmd, List<ReplaceItem> allRules, string filepattern)
        {
            this.cmd = cmd;
            this.filefilter = filepattern;
            this.rules = allRules.FindAll(r => r.cmdKey.Contains(cmd) ||  Regex.IsMatch(r.pattern, cmd, regOptions));
            msgs = new List<string>();
        }

        #region Replace File
        public void ReplaceFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath)) return;
            msgs.Clear();

            DirectoryInfo di = new DirectoryInfo(folderPath);
            ReplaceFolder(di);
        }
        private void ReplaceFolder(DirectoryInfo di)
        {
            ReplaceFiles(di.GetFiles());
            foreach (var sdi in di.GetDirectories())
            {
                ReplaceFolder(sdi);
            }
        }
        private void ReplaceFiles(FileInfo[] files)
        {
            foreach (FileInfo file in files)
            {
                if (file.Extension.EndsWith("bak")) continue;
                if (!string.IsNullOrEmpty(this.filefilter))
                {
                    if (!Regex.IsMatch(file.FullName, this.filefilter)) continue;
                }

                ReplaceFile(file.FullName);
            }
        }
        public void ReplaceFile(string file)
        {
            if (!File.Exists(file)) return;
            
            this.currentfile = file;
            this.currentLineNo = 0;
            string newContent = string.Empty;
            foreach (var rule in this.rules) 
            {
                if (rule.isSkipFile(file)) continue;
                
                if (rule.byLines)
                {
                    string[] lines = File.ReadAllLines(file, FileHelper.Encoding);
                    rule.beforeReplace(lines);
                    if (rule.replaceLines(lines))
                    {
                        FileInfo fi = new FileInfo(file);
                        File.WriteAllLines(file, lines, FileHelper.Encoding);
                    }
                    newContent = string.Join("\n", lines);
                }
                else
                {
                    string content = File.ReadAllText(file, FileHelper.Encoding);
                    rule.beforeReplace(content);
                    newContent = rule.replaceText(content);
                    if (!content.Equals(newContent))
                    {
                        FileInfo fi = new FileInfo(file);
                        File.WriteAllText(file, newContent, FileHelper.Encoding);
                    }
                }

                //特殊コマンドを除き、漏れはないか？(?:^|[^\.\w])substr\b
                if (!Regex.IsMatch(this.cmd, @":\w+"))
                {
                    MatchCollection ms = Regex.Matches(newContent, @"(?:^|[^\.\w])" + this.cmd + @"\b[^\n]+", regOptions);
                    foreach (Match m in ms)
                    {
                        if (!m.Value.StartsWith("."))
                        {
                            msgs.Add(string.Format("{0}\t{1}\t{2}", currentfile, m.Value, "★ERROR"));
                        }
                    }
                }
            }
        }
        
        //============================================================================
        #endregion
    }
}
