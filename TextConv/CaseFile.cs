using HtmlAgilityPack;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;

namespace TextConv
{
    public class CaseFile
    {
        public string title;
        public string titleText;
        public string FileName;
        public string subFileName;
        public List<CaseItem> listNode = new List<CaseItem>();

        private HtmlDocument htmlDoc;
        private static string subFilePathSetting = Config.GetAppSettingValue("subHtmlPath");
        public CaseFile(string htmlPath)
        {
            this.htmlDoc = new HtmlDocument();
            this.htmlDoc.Load(htmlPath);
            this.subFileName = UtilWxg.GetMatchGroup(htmlPath, subFilePathSetting, 1);

            FileInfo fi = new FileInfo(htmlPath);
            this.FileName = UtilWxg.GetMatchGroup(fi.Name, @"(.+)\.\w+$", 1);

            GetTitle();
        }

        private void GetTitle()
        {
            if (string.IsNullOrEmpty(title))
            {
                var ns = htmlDoc.DocumentNode.SelectNodes("//title");
                if(ns != null)
                {
                    title = ns.First().InnerText;
                    title = WebUtility.HtmlDecode(title);
                    titleText = UtilWxg.GetMatchGroup(title, @"\>(\w+)", 1);
                }
            } 
        }
        public void Parse(List<XpathItem> ruleItems)
        {
            foreach (var item in ruleItems)
            {
                var ns = htmlDoc.DocumentNode.SelectNodes(item.nameXpath);
                if (ns == null) continue;
                foreach(var node in ns)
                {
                    CaseItem ci = new CaseItem();
                    ci.title = this.title;
                    ci.subpath = this.subFileName;
                    ci.refresh(item, node);
                    if (item.textName.Contains("xpath"))
                    {
                        string textXpath = string.Format(item.textXpath, ci.eventKey);
                        var n2 = htmlDoc.DocumentNode.SelectNodes(textXpath);
                        if(n2 != null)
                        {
                            string innerText = n2.First().InnerText;
                            if(Regex.IsMatch(innerText, @"^\w+"))
                            {
                                ci.eventName = WebUtility.HtmlDecode(innerText);
                            }
                        }
                    }
                    ci.ToCaseDesc(item, node);

                    listNode.Add(ci);
                }
            }
        }
        
        public void Export()
        {
            string filePath = Config.GetAppSettingValue("caseFilePath");
            filePath = UtilWxg.ReplaceKeyValue(filePath, "title", titleText);
            
            filePath = UtilWxg.ReplaceKeyValue(filePath, "filename", this.FileName);
            string[] lines = listNode.Select(ci => ci.ToStringLine()).ToArray();
            File.WriteAllLines(filePath, lines);
        }
    }
}
