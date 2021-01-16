using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace TextConv
{
    [ClassInterface(ClassInterfaceType.None)]
    public class Xmler
    {
        public static string GetAttribute(XmlNode node, string name)
        {
            if (node.Attributes == null) return string.Empty;

            XmlNode attr = node.Attributes.GetNamedItem(name);
            if (attr != null)
            {
                return attr.Value;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetAppSettingValue(string key)
        {
            return GetAppSettingValue(key, string.Empty);
        }
        public static string GetAppSettingValue(string key, string defaultvalue)
        {
            string value = string.Empty;
            if (AppDomain.CurrentDomain.FriendlyName != null)
            {
                value = ConfigurationManager.AppSettings.Get(key);
            }

            if (string.IsNullOrEmpty(value))
            {
                value = defaultvalue;
            }
            return value;
        }
    }
}
