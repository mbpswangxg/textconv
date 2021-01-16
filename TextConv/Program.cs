using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace TextConv
{
    class Program
    {
        static void Main(string[] args)
        {
            //args check 
            //==============================================================
            if (args.Length < 2)
            {
                Console.WriteLine("TextConv -c COMMAND_KEY.");
                return;
            }
            string cmd = getCmdValue("-c", args);
            if (string.IsNullOrEmpty(cmd)) {
                Console.WriteLine("TextConv -c COMMAND_KEY");
                return;
            }
            string srcfile = getCmdValue("-f", args);
            
            string folder = Xmler.GetAppSettingValue("srcfolder");
            if (string.IsNullOrEmpty(folder))
            {
                Console.WriteLine("App.config setting srcfolder is required.");
                return;
            }

            //==============================================================
            List<ReplaceItem> items = new List<ReplaceItem>();
            readRegfile(items);
            
            RexgexUtils utils = new RexgexUtils(cmd, items, srcfile);
            utils.ReplaceFolder(folder);
            
            foreach(var e in utils.msgs)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("ERROR COUNT: {0}, SKIP COUNT: {1}", 
                utils.msgs.Count(r=>r.EndsWith("ERROR")),
                utils.msgs.Count(r => r.EndsWith("SKIP")));
        }
        
        private static string getCmdValue(string cmdPattern, string[] args) 
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
        private static void readRegfile(List<ReplaceItem> items) 
        {
            string[] lines = File.ReadAllLines("regfile.txt");
            string splitWords = Xmler.GetAppSettingValue("splitwords", ";;;");
            foreach(var line in lines) 
            {
                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith("#")) continue;
                
                string[] words = line.Split(splitWords.ToCharArray());
                items.Add(new ReplaceItem(words));
            }
        }


    }
}
