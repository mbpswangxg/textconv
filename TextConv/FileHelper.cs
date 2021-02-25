using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace TextConv
{
    class FileHelper
    {
        public static void Rename(string sourceFileName, string destFileName)
        {
            // old file not exits 
            if (!File.Exists(sourceFileName)) return;

            string oldfolder = Path.GetDirectoryName(sourceFileName);
            string newfolder = Path.GetDirectoryName(destFileName);
            if (string.IsNullOrEmpty(newfolder))
            {
                destFileName = Path.Combine(oldfolder, destFileName);
            }

            // new file has exited
            if (File.Exists(destFileName)) return;

            File.Move(sourceFileName, destFileName);
        }
        public static void FillFromFile(string file, Dictionary<string, string> caseDescMap)
        {
            if (!File.Exists(file)) return;

            string[] lines = File.ReadAllLines(file, Config.Encoding);
            foreach (string line in lines)
            {
                //空行を飛ばす
                if (Regex.IsMatch(line, @"^\s*$")) continue;

                //コメント行を飛ばす
                if (Regex.IsMatch(line, @"^(#|;|\-\-|\/\/)")) continue;
                FillMap(line, caseDescMap);
            }
        }
        public static void FillMap(string line, Dictionary<string, string> caseDescMap)
        {
            string[] words = Regex.Split(line, @"[\t,;=]+");
            if (words.Length > 1)
            {
                if (caseDescMap.ContainsKey(words[0]))
                {
                    throw new Exception(string.Format("dupliated:{0}, line:{1}", words[0], line));
                }
                caseDescMap.Add(words[0], words[1]);
            }
        }

        public static void FillFromFile(string file, ICollection<string> myset)
        {
            if (!File.Exists(file)) return;

            string[] lines = File.ReadAllLines(file, Config.Encoding);
            foreach (string line in lines)
            {
                //空行を飛ばす
                if (Regex.IsMatch(line, @"^\s*$")) continue;

                //コメント行を飛ばす
                if (Regex.IsMatch(line, @"^(#|;|\-\-|\/\/)")) continue;
                FillSet(line, myset);
            }
        }

        public static void FillSet(string content, ICollection<string> destSet)
        {
            string[] words = Regex.Split(content, @"[\t,;]+");
            foreach (var w in words)
            {
                destSet.Add(w);
            }
        }

    }
}
