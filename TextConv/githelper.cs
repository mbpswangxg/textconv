using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TextConv
{
    public class githelper
    {
        public static string UserName
        {
            get
            {
                return Xmler.GetAppSettingValue("gitusername");
            }
        }
        public static string UserMail
        {
            get
            {
                return Xmler.GetAppSettingValue("gitusermail");
            }
        }
        public static string RepoPath
        {
            get
            {
                return Xmler.GetAppSettingValue("gitrepopath");
            }
        }

        protected static int executeCommand(string command, string arguments = "")
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(command)
                {
                    Arguments = arguments,
                    UseShellExecute = false,
                }
            };
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }

        public static void gitCommand(string arguments)
        {
            if (executeCommand("git", arguments) != 0)
            {
                throw new Exception("git command error");
            }
        }

    }
}
