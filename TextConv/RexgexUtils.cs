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
        private ReplaceItem currentRule = null;
        private RegexOptions regOptions = RegexOptions.IgnoreCase | RegexOptions.Multiline;
        private string srcfile = string.Empty;
        
        public List<string> msgs;

        public RexgexUtils(string cmd, List<ReplaceItem> allRules, string filepattern)
        {
            this.cmd = cmd;
            this.srcfile = filepattern;
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
                if (!string.IsNullOrEmpty(this.srcfile))
                {
                    if (!Regex.IsMatch(file.FullName, this.srcfile)) continue;
                }

                ReplaceFile(file.FullName);
            }
        }
        public void ReplaceFile(string file)
        {
            if (!File.Exists(file)) return;
            
            this.currentfile = file;
            string newContent = string.Empty;
            foreach (var rule in this.rules) 
            {
                if (rule.isSkipFile(file)) continue;
                
                if (rule.byLines)
                {
                    string[] lines = File.ReadAllLines(file, FileHelper.Encoding);
                    rule.beforeReplace(lines);
                    if (replaceAllLines(lines, rule))
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
                    newContent = replaceAllText(content, rule);
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
        private string replaceAllText(string content, ReplaceItem rule)
        {
            //==============================
            this.currentRule = rule;
            if (!rule.ignoreCase)
            {
                regOptions = RegexOptions.Multiline;
            }
            else
            {
                regOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
            }

            Regex reg = new Regex(rule.pattern, regOptions);
            if (reg.IsMatch(content))
            {
                content = reg.Replace(content, MatchReplacer);
            }
            //==============================
            return content;
        }

        private bool replaceAllLines(string[] lines, ReplaceItem rule)
        {
            //==============================
            this.currentRule = rule;
            if (!rule.ignoreCase)
            {
                regOptions = RegexOptions.Singleline;
            }
            for (int i = 0; i < lines.Length; i++) 
            {
            }
                string nline = string.Empty;
            bool hasChanged = false;
            Regex reg = new Regex(rule.pattern, regOptions);
            for (int i = 0; i < lines.Length;i++)
            {
                nline = lines[i];
                //if (!string.IsNullOrEmpty(rule.excludePattern))
                //{
                //    //対象外範囲の場合、該当行を飛ばす
                //    if (Regex.IsMatch(nline, rule.excludePattern, regOptions)) 
                //    {
                //        msgs.Add(string.Format("{0}:{1}\t{2}\t{3}", currentfile, i+1, nline, "★SKIP"));
                //        continue;
                //    }
                //}
                    
                while (Regex.IsMatch(nline, rule.pattern, regOptions))
                {   
                    nline = reg.Replace(nline, MatchReplacer);
                    hasChanged = true;
                }
                lines[i] = nline;
            }
            
            return hasChanged;
        }
        

        private string MatchReplacer(Match m) 
        {
            string oldV = m.Value;
            string newV = m.Value;

            if (string.IsNullOrEmpty(currentRule.repCmdKey)
                || currentRule.repCmdKey.Equals("null") 
                || currentRule.repCmdKey.Equals("repfile"))
            {
                newV = Regex.Replace(m.Value, currentRule.pattern, currentRule.replacement, regOptions);
            }
            else if (currentRule.repCmdKey.Contains("UCASE_GROUP"))
            {
                for (int i = 0; i < m.Groups.Count; i++)
                {
                    if (currentRule.repGroup.Contains(i.ToString()))
                    {
                        foreach (Capture cpt in m.Groups[i].Captures)
                        {
                            newV = Regex.Replace(newV, cpt.Value, cpt.Value.ToUpper());
                        }
                    }
                }
            }
            else if (currentRule.repCmdKey.Contains("LCASE_GROUP"))
            {
                for (int i = 0; i < m.Groups.Count; i++)
                {
                    if (currentRule.repGroup.Contains(i.ToString()))
                    {
                        foreach (Capture cpt in m.Groups[i].Captures)
                        {
                            newV = Regex.Replace(newV, cpt.Value, cpt.Value.ToLower());
                        }
                    }
                }
            }
            else if (currentRule.repCmdKey.Contains("DECODE_GROUP"))
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
                    currentRule.repCmdKey, currentRule.repGroup, currentRule.replacement);

                newV = Regex.Replace(m.Value, currentRule.pattern, currentRule.replacement, regOptions);
            }
            
            Console.WriteLine("{0}\t{1}\t{2}", currentfile, oldV, newV);
            return newV;
        }
        
        //============================================================================
        #endregion
    }
}
