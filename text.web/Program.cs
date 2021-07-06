using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Text.Common;

namespace Text.Web
{
    public class Program
    {
        private static string resultFolder = string.Empty;
        static void Main(string[] args)
        {
            //args check 
            //==============================================================
            //string srcfolder = string.Empty;
            //if (args.Contains("-d"))
            //{
            //    srcfolder = getValue("-d", args);
            //}
            string srcFile = string.Empty;
            
            if (args.Contains("-f"))
            {
                srcFile = getValue("-f", args);
            }

            //if (string.IsNullOrEmpty(srcfolder) && string.IsNullOrEmpty(srcFile))
            //{
            //    Console.WriteLine("[-d srcfolder] or [-f srcfile] is required.");
            //    return;
            //}
            //==============================================================
            if (args.Contains("-web"))
            {
                WebRunner wr = new WebRunner();
                if (!string.IsNullOrEmpty(srcFile))
                {
                    wr.Run(srcFile);
                }
                //else if (!string.IsNullOrEmpty(srcfolder))
                //{
                //    wr.RunBatch(srcfolder);
                //}
                return;
            }
            Console.WriteLine("text.web.exe -web -f filepath ");

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

        

    }
}
