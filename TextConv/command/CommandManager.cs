using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextConv
{
    public class CommandManager
    {
        private List<ICommand> Commands = new List<ICommand>();
        public void ExcuteAllCommands()
        {
            Commands.ForEach(x => x.Execute());
        }
        public void ParseCommandArgs(string[] args)
        {
            if (args.Contains("-grep"))
            {
                string file = getValue("-f", args);
                string pattern = getValue("-p", args);
                bool ignoreCase = args.Contains("-i");
                bool multiline = args.Contains("-m");

                ICommand command = new GrepFileCommand(file, pattern, ignoreCase, multiline);
                AddCommand(command);
            }
        }
        public void AddCommand(ICommand command)
        {
            Commands.Add(command);
        }
        public void ClearCommands()
        {
            Commands.Clear();
        }

        private static string getValue(string cmdPattern, string[] args)
        {
            int x = args.ToList().IndexOf(cmdPattern);
            if (x > -1)
            {
                string v = args[x + 1];
                if (Regex.IsMatch(v, @"^--?[\w\-]+$"))
                {
                    return "";
                }
                else
                {
                    return v;
                }
            }
            else
            {
                return "";
            }
        }
    }
}
