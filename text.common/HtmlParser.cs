using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace TextConv
{
    public class HtmlParser
    {
        #region static methods
        public static string HistoryFile
        {
            get
            {
                return Config.GetAppSettingValue2("history.file", "history.txt");
            }
        }

        public static string GetInnerText(string htmlpath, string findString)
        {
            Dictionary<string, HtmlNodeCollection> lstNode = GetNodes(htmlpath, findString);

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, HtmlNodeCollection> kv in lstNode)
            {
                foreach (var n in kv.Value)
                {
                    foreach (var atr in n.Attributes)
                    {
                        sb.Append(atr.Name).Append(":").Append(atr.Value).Append(",");
                    }
                    sb.Append("InnerText").Append(":").Append(n.InnerText);
                    sb.AppendLine();
                }
            }

            return WebUtility.HtmlDecode(sb.ToString());
        }
        
        public static string GetOutHtml(string htmlpath, string findString)
        {
            Dictionary<string, HtmlNodeCollection> lstNode = GetNodes(htmlpath, findString);

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, HtmlNodeCollection> kv in lstNode)
            {
                foreach (var n in kv.Value)
                {
                    if (n.Attributes.Contains("type"))
                    {
                        sb.Append("type:").Append(n.Attributes["type"].Value).Append(",");
                    }
                    sb.Append(n.OuterHtml);
                    sb.AppendLine();
                }
            }

            return WebUtility.HtmlDecode(sb.ToString());
        }
        public static Dictionary<string, HtmlNodeCollection> GetNodes(string htmlpath, string findString)
        {
            var htmlDoc = new HtmlDocument();
            if(Regex.IsMatch(htmlpath, @"^[a-zA-Z]:\\"))
            {
                htmlDoc.Load(htmlpath, Config.Encoding);
            }else if (Regex.IsMatch(htmlpath, @"^http[s]?:"))
            {
                HtmlWeb web = new HtmlWeb();
                htmlDoc = web.Load(htmlpath);
            }
            else
            {
                htmlDoc.LoadHtml(htmlpath);
            }

            string[] finds = Regex.Split(findString, @";|\n");
            Dictionary<string, HtmlNodeCollection> lstNode = new Dictionary<string, HtmlNodeCollection>();
            foreach (var key in finds)
            {
                if (string.IsNullOrEmpty(key)) continue;

                var ns = htmlDoc.DocumentNode.SelectNodes(key);
                if (ns == null) continue;
                lstNode.Add(key, ns);
            }

            return lstNode;
        }
        public static string GetHtml(string Url)
        {
            if (!Regex.IsMatch(Url, @"^http[s]?:")) return string.Format("url header should be http[s]. inputUrl:{0}", Url);

            using (WebClient client = new WebClient())
            {
                client.Encoding = Config.Encoding;
                try
                {
                    return client.DownloadString(Url);
                }catch(Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        public static string GetAttText(string htmlpath, string findString)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.Load(htmlpath);
            var nodes = htmlDoc.DocumentNode.SelectNodes(findString);
            if (nodes == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (var n in nodes)
            {
                foreach (var atr in n.Attributes)
                {
                    sb.Append(atr.Name).Append(":").Append(atr.Value).Append(",");
                }
                sb.Append("InnerText").Append(":").Append(n.InnerText);
                sb.AppendLine();
            }

            return WebUtility.HtmlDecode(sb.ToString());
        }
        #endregion
    }
}
