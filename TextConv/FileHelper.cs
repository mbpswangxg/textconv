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
        public static void FillFromFile(string file, Dictionary<string, string> mymap)
        {
            string splitWords = Config.GetAppSettingValue("splitwords");
            FillFromFile(file, mymap, splitWords);
        }
        public static void FillFromFile(string file, Dictionary<string, string> mymap, string splitWords)
        {
            if (!File.Exists(file)) return;

            string[] lines = File.ReadAllLines(file, Config.Encoding);
            foreach (string line in lines)
            {
                //空行を飛ばす
                if (Regex.IsMatch(line, @"^\s*$")) continue;

                //コメント行を飛ばす
                if (Regex.IsMatch(line, @"^#")) continue;
                FillMap(line, mymap, splitWords);
            }
        }
        public static void FillMap(string line, Dictionary<string, string> mymap, string splitWords)
        {
            string[] words = Regex.Split(line, splitWords);
            if (words.Length > 1)
            {
                if (mymap.ContainsKey(words[0]))
                {
                    throw new Exception(string.Format("dupliated:{0}, line:{1}", words[0], line));
                }
                mymap.Add(words[0], words[1]);
            }
        }

        public static void FillFromFile(string file, ICollection<string> myset)
        {
            string splitWords = Config.GetAppSettingValue("splitwords");
            FillFromFile(file, myset, splitWords);
        }

        public static void FillFromFile(string file, ICollection<string> myset, string splitWords)
        {
            if (!File.Exists(file)) return;

            string[] lines = File.ReadAllLines(file, Config.Encoding);
            foreach (string line in lines)
            {
                //空行を飛ばす
                if (Regex.IsMatch(line, @"^\s*$")) continue;

                //コメント行を飛ばす
                if (Regex.IsMatch(line, @"^#")) continue;
                FillSet(line, myset, splitWords);
            }
        }

        public static void FillSet(string line, ICollection<string> destSet, string splitWords)
        {
            if (string.IsNullOrEmpty(splitWords))
            {
                destSet.Add(line);
                return;
            }

            string[] words = Regex.Split(line, splitWords);
            foreach (var w in words)
            {
                destSet.Add(w);
            }
        }

    }
}
