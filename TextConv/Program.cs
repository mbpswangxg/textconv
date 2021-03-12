using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
                Console.WriteLine("TextConv [-p PATTERN] [-r REPLACEMENT] [-d srcfolder] [-f srcfile]");
                Console.WriteLine("TextConv [-c COMMAND_KEY] [-x XPATH] [-d srcfolder] [-f srcfile]");
                return;
            }
            string cmd = getValue("-c", args);

            string srcfolder = getValue("-d", args);
            if (string.IsNullOrEmpty(srcfolder))
            {
                srcfolder = Config.GetAppSettingValue("srcfolder");
            }
            resultFolder = UtilWxg.GetMatchGroup(srcfolder, @"(\w+)\\*$", 1);

            string srcFile = getValue("-f", args);
            if (string.IsNullOrEmpty(srcfolder) && string.IsNullOrEmpty(srcFile))
            {
                Console.WriteLine("App.config setting srcfolder is required.");
                return;
            }

            //==============================================================
            if (args.Contains("-x")) 
            {
                List<XPathRuleItem> rules = new List<XPathRuleItem>();
                string ruleFolderPath = Config.GetAppSettingValue("xpath.rule.yml");
                if (Directory.Exists(ruleFolderPath))
                {
                    LoadYmlRules(rules, ruleFolderPath, cmd, true);
                }
                HtmlParseFolder(srcfolder, rules);   
            }

            //==============================================================
            if (args.Contains("-c") || args.Contains("-p"))
            {
                List<ReplaceRule> repRules = new List<ReplaceRule>();
                string ruleFolderPath = Config.GetAppSettingValue("replace.rule.yml");
                if (Directory.Exists(ruleFolderPath) && !string.IsNullOrEmpty(cmd))
                {
                    LoadYmlRules(repRules, ruleFolderPath, cmd, false);
                }

                ReplaceRuleItem ri = new ReplaceRuleItem();
                ri.pattern = getValue("-p", args);
                ri.replacement = getValue("-r", args);
                if (!string.IsNullOrEmpty(ri.pattern))
                {
                    string content = getValue("-input", args);
                    if (!string.IsNullOrEmpty(content))
                    {
                        Console.WriteLine(ri.replaceText(content));
                    }
                    else
                    {
                        ReplaceRule rule = new ReplaceRule();
                        rule.rules.Add(ri);
                        repRules.Add(rule);
                    }
                }
                foreach (ReplaceRule rule in repRules)
                {
                    rule.ReplaceFolder(srcfolder);
                    rule.ReplaceFile(srcFile);
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

        #region Html Parser for export
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
        
        private static void LoadYmlRules<T>(List<T> items, string ruleFilePath, string cmd, bool toAll)
        {
            var deserializer = new Deserializer();
            try
            {
                foreach(var filepath in Directory.GetFiles(ruleFilePath))
                {
                    using (StreamReader reader = File.OpenText(filepath))
                    {
                        string name = UtilWxg.GetMatchGroup(filepath, @"\\*(\w+)\.\w+", 1);
                        
                        if (string.IsNullOrEmpty(cmd))
                        {
                            if (toAll)
                            {
                                T item = deserializer.Deserialize<T>(reader);
                                items.Add(item);
                            }
                        }
                        else if (name.Equals(cmd))
                        {
                            T item = deserializer.Deserialize<T>(reader);
                            items.Add(item);
                        }
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
