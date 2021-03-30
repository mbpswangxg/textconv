using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace TextConv
{
    public class YmlLoader
    {
        public static void Load<T>(List<T> items, string ymlDirectoryPath)
        {
            Load(items, ymlDirectoryPath, string.Empty);
        }

        public static void Load<T>(List<T> items, string ymlDirectoryPath, string filename)
        {
            if (string.IsNullOrEmpty(ymlDirectoryPath))
            {
                ymlDirectoryPath = Config.GetAppSettingValue("web.rule.yml");
            }

            if (string.IsNullOrEmpty(ymlDirectoryPath)) return;
            if (!Directory.Exists(ymlDirectoryPath))
            {
                Console.WriteLine("Can't found file: {0}", ymlDirectoryPath);
                return;
            }

            var deserializer = new Deserializer();
            foreach (var filepath in Directory.GetFiles(ymlDirectoryPath))
            {
                if (!Regex.IsMatch(filepath, ".(yml|yaml)$")) continue;

                LoadFromFile(items, filepath);
            }

            DirectoryInfo di = new DirectoryInfo(ymlDirectoryPath);
            foreach (var sdi in di.GetDirectories())
            {
                Load(items, sdi.FullName, filename);
            }
        }

        public static void LoadFromFile<T>(List<T> items, string ymlFilePath)
        {
            if (string.IsNullOrEmpty(ymlFilePath)) return;
            if (!File.Exists(ymlFilePath)) 
            {
                Console.WriteLine("Can't found file: {0}", ymlFilePath);
                return;
            }

            var deserializer = new Deserializer();

            using (StreamReader reader = File.OpenText(ymlFilePath))
            {
                string name = UtilWxg.GetMatchGroup(ymlFilePath, @"\\*(\w+)\.\w+", 1);

                T item = deserializer.Deserialize<T>(reader);
                items.Add(item);

                Type typeX = item.GetType();
                FieldInfo fi = typeX.GetField("name");
                if (fi != null)
                {
                    fi.SetValue(item, name);
                }

                MethodInfo mi = typeX.GetMethod("Init");
                if (mi != null)
                {
                    mi.Invoke(item, null);
                }
            }
        }
    }
}
