using System.Xml;

namespace TextConv
{
    public class XmlParser
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
    }
}
