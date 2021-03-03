using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;
using YamlDotNet.Serialization;

namespace TextConv
{
    public class Program
    {
        private static string resultFolder = string.Empty;
        static void Main(string[] args)
        {
            //args check 
            //==============================================================
            if (args.Length < 2)
            {
                Console.WriteLine("TextConv [-c COMMAND_KEY] [-r ruleFile] [-d srcfolder] [-f srcfile]");
                return;
            }
            string cmd = getValue("-c", args);
            
            string folder = getValue("-d", args);
            if (string.IsNullOrEmpty(folder))
            {
                folder = Config.GetAppSettingValue("srcfolder");
            }
            string destFile = getValue("-f", args);
            if (string.IsNullOrEmpty(folder) && string.IsNullOrEmpty(destFile))
            {
                Console.WriteLine("App.config setting srcfolder is required.");
                return;
            }
            string ruleFile = getValue("-r", args);
            if (string.IsNullOrEmpty(ruleFile))
            {
                ruleFile = Config.GetAppSettingValue2("regfile", "regfile.txt");
            }
            //==============================================================
            string xfolder = getValue("-x", args);
            if (!string.IsNullOrEmpty(xfolder))
            {
                string ruleFolderPath = Config.GetAppSettingValue("xpath.rule.yml");
                HtmlParseFolder(xfolder, ruleFolderPath);
            }
            else
            {
                if (!string.IsNullOrEmpty(folder))
                {
                    ReplaceFolder(folder, ruleFile, cmd);
                }
                if (!string.IsNullOrEmpty(destFile))
                {
                    RelaceFile(destFile, ruleFile, cmd);
                }
            }
        }
        
        private static string getValue(string cmdPattern, string[] args) 
        {
            int x = args.ToList().IndexOf(cmdPattern);
            if (x > -1)
            {
                return args[x + 1];
            }
            else {
                return "";
            }
        }

        #region Replace Folder/File
        private static void readRegfile(List<ReplaceItem> items, string ruleFilePath)
        {
            string[] lines = File.ReadAllLines(ruleFilePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith("#")) continue;

                items.Add(new ReplaceItem(line));
            }
        }
        public static void ReplaceFolder(string folder, string ruleFile)
        {
            ReplaceFolder(folder, ruleFile, string.Empty);
        }
        public static void ReplaceFolder(string folder, string ruleFile, string cmd)
        {
            List<ReplaceItem> rules = new List<ReplaceItem>();
            readRegfile(rules, ruleFile);
            if (!string.IsNullOrEmpty(cmd))
            {
                rules.RemoveAll(r => !r.cmdKey.Equals(cmd, StringComparison.OrdinalIgnoreCase)
                                  && !Regex.IsMatch(r.pattern, cmd, RegexOptions.IgnoreCase));
            }
            if (Directory.Exists(folder) && rules.Count > 0)
            {
                ReplaceFolder(folder, rules);
            }
        }
        public static void ReplaceFolder(string folderPath, List<ReplaceItem> rules)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);

            //replace by file 
            foreach (FileInfo file in di.GetFiles())
            {
                foreach (var rule in rules)
                {
                    rule.ReplaceFile(file.FullName);
                }
            }

            //go to 
            foreach (var sdi in di.GetDirectories())
            {
                ReplaceFolder(sdi.FullName, rules);
            }
        }

        public static void RelaceFile(string destfile, string ruleFile)
        {
            RelaceFile(destfile, ruleFile, string.Empty);
        }
        public static void RelaceFile(string destfile, string ruleFile, string cmd)
        {
            List<ReplaceItem> rules = new List<ReplaceItem>();
            readRegfile(rules, ruleFile);
            if (!string.IsNullOrEmpty(cmd))
            {
                rules.RemoveAll(r => !r.cmdKey.Equals(cmd, StringComparison.OrdinalIgnoreCase)
                                  && !Regex.IsMatch(r.pattern, cmd, RegexOptions.IgnoreCase));
            }
            
            if (File.Exists(destfile) && rules.Count > 0)
            {
                RelaceFile(destfile, rules);
            }
        }
        public static void RelaceFile(string destfile, List<ReplaceItem> rules)
        {
            FileInfo file = new FileInfo(destfile);
            if (!file.Exists) return;

            foreach (var rule in rules)
            {
                rule.ReplaceFile(file.FullName);
            }
        }

        #endregion

        #region Html Parser for export
        public static void HtmlParseFolder(string folder, string ruleFolderPath)
        {
            if (!Directory.Exists(folder))
            {
                Console.WriteLine("★Error★: folder not found:[{0}].", folder);
                return;
            }

            if (!Directory.Exists(ruleFolderPath))
            {
                Console.WriteLine("★Error★: ruleFolder not found:[{0}].", ruleFolderPath);
                return;
            }

            List<XPathRuleItem> rules = new List<XPathRuleItem>();
            LoadXPathRules(rules, ruleFolderPath);
            HtmlParseFolder(folder, rules);
        }
        private static void HtmlParseFolder(string folder, List<XPathRuleItem> ruleItems)
        {
            string ext = Config.GetAppSettingValue2("xpath.ext", ".(html?|xml)$");
            foreach (string filePath in Directory.GetFiles(folder))
            {
                if (!Regex.IsMatch(filePath, ext)) continue;
                HtmlParseFile(filePath, ruleItems);
            }
            foreach (string filePath in Directory.GetDirectories(folder))
            {
                HtmlParseFolder(filePath, ruleItems);
            }
        }


        private static void HtmlParseFile(string filePath, List<XPathRuleItem> ruleItems)
        {
            CaseFile cf = new CaseFile(filePath);
            cf.Parse(ruleItems);
            cf.Export(resultFolder);
            Console.WriteLine(string.Format("{0}:casecount={1}",cf.exportFile, cf.listNode.Count));
            foreach(var msg in cf.errmsgs)
            {
                Console.WriteLine(msg);
            }
        }
        
        private static void LoadXPathRules(List<XPathRuleItem> items, string ruleFilePath)
        {
            var deserializer = new Deserializer();
            try
            {
                
                foreach(var filepath in Directory.GetFiles(ruleFilePath))
                {
                    using (StreamReader reader = File.OpenText(filepath))
                    {
                        XPathRuleItem r = deserializer.Deserialize<XPathRuleItem>(reader);
                        items.Add(r);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
        #endregion
    }
}
