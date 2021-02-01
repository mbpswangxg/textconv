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

        public RexgexUtils(string cmd, List<ReplaceItem> allRules)
        {
            this.cmd = cmd;
            this.rules = allRules.FindAll(r => r.cmdKey.Equals(cmd, StringComparison.OrdinalIgnoreCase) || Regex.IsMatch(r.pattern, cmd,RegexOptions.IgnoreCase));
        }

        #region Replace File
        public void ReplaceFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath)) return;
            
            DirectoryInfo di = new DirectoryInfo(folderPath);
        
            //replace by file 
            foreach (FileInfo file in di.GetFiles())
            {
                foreach (var rule in this.rules)
                {
                    rule.ReplaceFile(file.FullName);
                }
            }

            //go to 
            foreach (var sdi in di.GetDirectories())
            {
                ReplaceFolder(sdi.FullName);
            }
        }

       /* public void ReplaceFile(string file)
        {
            if (!File.Exists(file)) return;
            
            string newContent = string.Empty;
            foreach (var rule in this.rules) 
            {
                if (rule.isSkipFile(file)) continue;
                rule.currentfile = file;
                
                if (!rule.multiLine)
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
        */
        //============================================================================
        #endregion
    }
}
