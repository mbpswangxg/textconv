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
            if (!Directory.Exists(ymlDirectoryPath)) return;

            var deserializer = new Deserializer();
            foreach (var filepath in Directory.GetFiles(ymlDirectoryPath))
            {
                if (!Regex.IsMatch(filepath, ".(yml|yaml)$")) continue;

                using (StreamReader reader = File.OpenText(filepath))
                {
                    string name = UtilWxg.GetMatchGroup(filepath, @"\\*(\w+)\.\w+", 1);

                    if (string.IsNullOrEmpty(filename) || name.Equals(filename))
                    {
                        T item = deserializer.Deserialize<T>(reader);
                        items.Add(item);

                        Type typeX = item.GetType();
                        FieldInfo fi = typeX.GetField("name");
                        if (fi != null) 
                        {
                            fi.SetValue(item, name);
                        }
                        MethodInfo mi = typeX.GetMethod("Init");
                        if(mi != null)
                        {
                            mi.Invoke(item, null);
                        }
                    }
                }
            }

            DirectoryInfo di = new DirectoryInfo(ymlDirectoryPath);
            foreach (var sdi in di.GetDirectories())
            {
                Load(items, sdi.FullName, filename);
            }
        }
    }
}
