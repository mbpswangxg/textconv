using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextConv
{
    public class GrepCommand : ICommand
    {
        public CommandManager Manager { get; set; }
        public string FilePath { get; set; }
        public string Pattern { get; set; }
        public bool IgnoreCase { get; set; }
        public bool Multiline { get; set; }

        private RegexOptions options = RegexOptions.None;
        private List<string> result = new List<string>();
        #region construction
        
        public GrepCommand(string pattern, bool IgnoreCase, bool multiline)
        {
            this.Pattern = pattern;
            this.IgnoreCase = IgnoreCase;
            this.Multiline = multiline;
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

            if (string.IsNullOrEmpty(this.FilePath))
            {
                Console.WriteLine("Error: filename [{0}] is unvaliable.!", this.FilePath);
                return;
            }
            if (string.IsNullOrEmpty(this.Pattern))
            {
                Console.WriteLine("Error: filename [{0}] is unvaliable.!", this.FilePath);
                return;
            }
            if (!File.Exists(this.FilePath))
            {
                Console.WriteLine("Error: file [{0}] not exist!", this.FilePath);
                return;
            }

            string[] lines = File.ReadAllLines(this.FilePath, Config.Encoding);
            Regex reg = new Regex(this.Pattern, this.options);
            StringBuilder buffer = new StringBuilder();
            for (int i= 0; i < lines.Length; i++)
            {
                MatchCollection mc = reg.Matches(lines[i]);
                foreach (Match m in mc)
                {
                    buffer.Clear();
                    string header = string.Format("{0}\t{1}\t{2}", this.FilePath, i, m.Value);
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
