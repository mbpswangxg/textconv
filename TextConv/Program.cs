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
                LoadYmlRules(rules, ruleFolderPath, cmd);
                HtmlParseFolder(srcfolder, rules);
                HtmlParseFile(srcFile, rules);
            }

            //==============================================================
            if (args.Contains("-c") || args.Contains("-p"))
            {
                List<ReplaceRule> repRules = new List<ReplaceRule>();
                string ruleFolderPath = Config.GetAppSettingValue("replace.rule.yml");
                LoadYmlRules(repRules, ruleFolderPath, cmd);

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
                ReplaceFolder(srcfolder, repRules);
                ReplaceFile(srcfolder, repRules);
            }
        }
        
        private static string getValue(string cmdPattern, string[] args) 
        {
            int x = args.ToList().IndexOf(cmdPattern);
            if (x > -1)
            {
                string v = args[x + 1];
                if(Regex.IsMatch(v, @"^--?[\w\-]+$"))
                {
                    return "";
                }
                else
                {
                    return v;
                }
            }
            else {
                return "";
            }
        }

        #region Html Parser for export
        private static void HtmlParseFolder(string folder, List<XPathRuleItem> ruleItems)
        {
            if (!Directory.Exists(folder)) return;

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
            if (!File.Exists(filePath)) return;

            CaseFile cf = new CaseFile(filePath);
            cf.Parse(ruleItems);
            cf.Export(resultFolder);
            Console.WriteLine(string.Format("{0}:casecount={1}",cf.exportFile, cf.listNode.Count));
            foreach(var msg in cf.errmsgs)
            {
                Console.WriteLine(msg);
            }
        }

        private static void ReplaceFolder(string folder, List<ReplaceRule> ruleItems)
        {
            if (!Directory.Exists(folder)) return;
            foreach (ReplaceRule rule in ruleItems)
            {
                rule.ReplaceFolder(folder);
            }
        }
        private static void ReplaceFile(string filePath, List<ReplaceRule> ruleItems)
        {
            if (!File.Exists(filePath)) return;
            foreach (ReplaceRule rule in ruleItems)
            {
                rule.ReplaceFile(filePath);
            }
        }

        private static void LoadYmlRules<T>(List<T> items, string ruleFolderPath, string cmd)
        {
            if (!Directory.Exists(ruleFolderPath)) return;
            
            var deserializer = new Deserializer();
            foreach (var filepath in Directory.GetFiles(ruleFolderPath))
            {
                if (!Regex.IsMatch(filepath, ".(yml|yaml)$")) continue;

                using (StreamReader reader = File.OpenText(filepath))
                {
                    string name = UtilWxg.GetMatchGroup(filepath, @"\\*(\w+)\.\w+", 1);

                    if (string.IsNullOrEmpty(cmd))
                    {
                        T item = deserializer.Deserialize<T>(reader);
                        items.Add(item);
                    }
                    else if (name.Equals(cmd))
                    {
                        T item = deserializer.Deserialize<T>(reader);
                        items.Add(item);
                    }
                }
            }

            DirectoryInfo di = new DirectoryInfo(ruleFolderPath);
            foreach (var sdi in di.GetDirectories())
            {
                LoadYmlRules(items, sdi.FullName, cmd);
            }
        }
        #endregion

    }
}
