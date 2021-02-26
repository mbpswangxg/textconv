using HtmlAgilityPack;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private string htmlPath;
        private HtmlDocument htmlDoc;
        private static string subFilePathSetting = Config.GetAppSettingValue("subHtmlPath");
        public CaseFile(string htmlPath)
        {
            this.htmlPath = htmlPath;
            this.htmlDoc = new HtmlDocument();

            this.htmlDoc.Load(File.OpenRead(htmlPath), true);
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
            listNode.Clear();

            CaseItem ci = new CaseItem();
            listNode.Add(ci);
            ci.No = listNode.Count;

            ci.title = this.title;
            ci.subpath = this.subFileName;
            ci.eventText = "初期表示";
            ci.caseDesc = "画面が表示される";

            foreach (var item in ruleItems)
            {
                var ns = htmlDoc.DocumentNode.SelectNodes(item.nameXpath);
                if (ns == null) continue;
                foreach(var node in ns)
                {
                    ci = new CaseItem();
                    listNode.Add(ci);
                    ci.No = listNode.Count;

                    ci.title = this.title;
                    ci.subpath = this.subFileName;
                    ci.refresh(item, node);
                    ci.ToCaseDesc(item, node);
                }
            }
        }
        
        public void Export()
        {
            string caseFileName = Config.GetAppSettingValue("caseFileName");
            caseFileName = UtilWxg.ReplaceKeyValue(caseFileName, "title", titleText);
            caseFileName = UtilWxg.ReplaceKeyValue(caseFileName, "filename", this.FileName);

            string[] lines = listNode.Select(ci => ci.ToStringLine()).ToArray();
            string exportFolder = Config.GetAppSettingValue2("exportFolder", "exportText");
            if (!exportFolder.EndsWith("\\")) exportFolder = exportFolder + "\\";
            if (!Directory.Exists(exportFolder))
            {
                Directory.CreateDirectory(exportFolder);
            }
            string filePath = exportFolder + caseFileName;
            File.WriteAllLines(filePath, lines);

            UtilWxg.Log(ToStringLine());
        }
        public string ToStringLine()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.htmlPath).Append(title).AppendLine();
            return sb.ToString();
        }
    }
}
