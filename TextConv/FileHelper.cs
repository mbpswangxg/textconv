using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TextConv
{
    class FileHelper
    {
        public static Encoding Encoding
        {
            get
            {
                string encoding = Xmler.GetAppSettingValue("encoding");
                if (string.IsNullOrEmpty(encoding))
                {
                    return Encoding.UTF8;
                }

                return Encoding.GetEncoding(encoding);
            }
        }

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
    }
}
