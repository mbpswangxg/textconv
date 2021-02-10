using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace TextConv
{
    public class Program
    {

        static void Main(string[] args)
        {
            //args check 
            //==============================================================
            if (args.Length < 2)
            {
                Console.WriteLine("TextConv [-c COMMAND_KEY] [-r ruleFile] [-d srcfolder]");
                return;
            }
            string cmd = getValue("-c", args);
            
            string folder = getValue("-d", args);
            if (string.IsNullOrEmpty(folder))
            {
                folder = Xmler.GetAppSettingValue("srcfolder");
            }
            if (string.IsNullOrEmpty(folder))
            {
                Console.WriteLine("App.config setting srcfolder is required.");
                return;
            }
            string ruleFile = getValue("-r", args);
            if (string.IsNullOrEmpty(ruleFile))
            {
                ruleFile = Xmler.GetAppSettingValue("regfile", "regfile.txt");
            }
            //==============================================================
            ReplaceFolder(folder, ruleFile, cmd);
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
        private static void readRegfile(List<ReplaceItem> items, string ruleFilePath) 
        {
            string[] lines = File.ReadAllLines(ruleFilePath);
            foreach(var line in lines) 
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
    }
}
