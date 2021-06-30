using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Text.Common;

namespace TextConv
{
    public class GrepFolderCommand : ICommand
    {
        public CommandManager Manager { get; set; }
        private string folderPath= string.Empty;
        private string pattern = string.Empty;
        private string searchPattern = string.Empty; 
        private RegexOptions options = RegexOptions.None;
        private List<string> result = new List<string>();
        #region construction
        
        public GrepFolderCommand(string fullPath, string searchPattern, string pattern, bool IgnoreCase, bool multiline)
        {
            this.folderPath = fullPath;
            this.pattern = pattern;
            this.searchPattern = searchPattern;
            if (IgnoreCase)
            {
                this.options |= RegexOptions.IgnoreCase;
            }
            if (multiline)
            {
                this.options |= RegexOptions.Multiline;
            }
        }

        #endregion

        public void Execute()
        {
            result.Clear();

            if (string.IsNullOrEmpty(this.folderPath))
            {
                Console.WriteLine("Error: filename [{0}] is unvaliable.!", this.folderPath);
                return;
            }
            if (string.IsNullOrEmpty(this.pattern))
            {
                Console.WriteLine("Error: filename [{0}] is unvaliable.!", this.folderPath);
                return;
            }
            if (!Directory.Exists(this.folderPath))
            {
                Console.WriteLine("Error: Directory [{0}] not exist!", this.folderPath);
                return;
            }

            string[] files = Directory.GetFiles(this.folderPath, this.searchPattern);
            //foreach(string file in files)
            //{

            //}
            //if(files.Length > 0)
            //{

            //}
            string[] lines = File.ReadAllLines(this.folderPath, Config.Encoding);
            Regex reg = new Regex(this.pattern, this.options);
            StringBuilder buffer = new StringBuilder();
            for (int i= 0; i < lines.Length; i++)
            {
                MatchCollection mc = reg.Matches(lines[i]);
                foreach (Match m in mc)
                {
                    buffer.Clear();
                    string header = string.Format("{0}\t{1}\t{2}", this.folderPath, i, m.Value);
                    buffer.Append(header);

                    if (m.Groups.Count > 1)
                    {
                        for (int x = 1;x< m.Groups.Count; x++)
                        {
                            buffer.Append("\t").Append("group[" + x + "]=").Append(m.Groups[x].Value);
                        }
                    }
                    result.Add(buffer.ToString());
                    Console.WriteLine(buffer);
                }
            }
        }
    }
}
