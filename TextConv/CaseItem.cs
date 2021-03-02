using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;


namespace TextConv
{
    public class CaseItem
    {
        public int No;
        public string title;
        public string subpath;
        public string eventKey;
        public string eventName;
        public string eventText;
        public string caseDesc;
        public string condition;

        private static string ptwords = @"\>([\w\s]+)";

        private void RefreshKeyName(XpathItem ruleItem, HtmlNode node)
        {
            setEventKeyByAttr(ruleItem, node, "element");
            setEventKeyByAttr(ruleItem, node, "name");
            setEventKeyByAttr(ruleItem, node, "value");
            setEventKeyByAttr(ruleItem, node, "onclick", @"\w+\('(\w+)'", 1);
            setEventKeyByAttr(ruleItem, node, "onclick", @"(\w+)\(", 1);
            setEventKeyByAttr(ruleItem, node, "href", @"\w+\('(\w+)'", 1);
            setEventKeyByAttr(ruleItem, node, "onclick", @"(\w+)\(", 1);

            if (ruleItem.name.Contains("button"))
            {
                setEventNameByAttr(ruleItem, node, "value");
            }

            setEventNameByAttr(ruleItem, node, "id");
            setEventNameByAttr(ruleItem, node, "name");
            setEventNameByInnerText(ruleItem, node);
            setEventNameByAttr(ruleItem, node, "value");

        }
        public void refresh(XpathItem ruleItem, HtmlNode node)
        {
            RefreshKeyName(ruleItem, node);

            if (!string.IsNullOrEmpty(this.eventKey) && ruleItem.wordMap.ContainsKey(this.eventKey))
            {
                this.eventName = ruleItem.wordMap[this.eventKey];
            }
            HtmlDocument htmlDoc = node.OwnerDocument;
            foreach (var xpath in ruleItem.textXpathSet)
            {
                string xpath2 = string.Format(xpath, eventKey);
                var n2 = htmlDoc.DocumentNode.SelectNodes(xpath2);
                if (n2 != null)
                {
                    HtmlNode firstNode = n2.First();
                    string innerText = firstNode.InnerText;
                    if (!string.IsNullOrEmpty(innerText))
                    {
                        this.eventName = WebUtility.HtmlDecode(innerText);
                        break;
                    }
                    RefreshKeyName(ruleItem, firstNode);
                    if (ruleItem.name.Contains("link"))
                    {
                        setEventNameByAttr(ruleItem, firstNode, "alt");
                    }
                    
                }
            }
            if (Regex.IsMatch(this.eventName, ptwords))
            {
                this.eventName = UtilWxg.GetMatchGroup(this.eventName, ptwords, 1);
            }
        }

        public void ToCaseDesc(XpathItem ruleItem, HtmlNode node)
        {
            if (ruleItem.name.Contains("sortlink"))
            {
                if (node.OuterHtml.Contains("ASC"))
                {
                    eventText = string.Format(ruleItem.eventText, eventName, "△");
                    caseDesc = UtilWxg.ReplaceKeyValue(ruleItem.caseDescFormat, "eventname", eventName);
                    caseDesc = UtilWxg.ReplaceKeyValue(caseDesc, "sorttype", "昇順");
                }
                else
                {
                    eventText = string.Format(ruleItem.eventText, eventName, "▽");
                    caseDesc = UtilWxg.ReplaceKeyValue(ruleItem.caseDescFormat, "eventname", eventName);
                    caseDesc = UtilWxg.ReplaceKeyValue(caseDesc, "sorttype", "降順");
                }
                return;
            }
            else if (ruleItem.name.Contains("calender"))
            {
                if (eventKey.EndsWith("_S"))
                {
                    eventName = eventName + " 開始";
                }
                else if(eventKey.EndsWith("_E"))
                {
                    eventName = eventName + " 終了";
                }
                eventText = string.Format(ruleItem.eventText, eventName);
            }
            else
            {
                eventText = string.Format(ruleItem.eventText, eventName);
            }
            
            if (Regex.IsMatch(eventName,@"\>\w+"))
            {
                eventName = UtilWxg.GetMatchGroup(eventName, @"\>(\w+)", 1);
            }

            if (!string.IsNullOrEmpty(eventKey) && ruleItem.caseDescMap.ContainsKey(eventKey))
            {
                caseDesc = UtilWxg.ReplaceKeyValue(ruleItem.caseDescMap[eventKey], "eventname", eventName);
            }
            else if (ruleItem.caseDescMap.ContainsKey(eventName))
            {
                caseDesc = UtilWxg.ReplaceKeyValue(ruleItem.caseDescMap[eventName], "eventname", eventName);
            }
            else if (!string.IsNullOrEmpty(ruleItem.caseDescFormat))
            {
                caseDesc = UtilWxg.ReplaceKeyValue(ruleItem.caseDescFormat, "eventname", eventName);
            }
        }

        private void setEventNameByAttr(XpathItem ruleItem, HtmlNode node, string attrName)
        {
            if (!string.IsNullOrEmpty(eventName)) return;
            if (!node.Attributes.Contains(attrName)) return;
            eventName = node.Attributes[attrName].Value;
        }
        private void setEventNameByInnerText(XpathItem ruleItem, HtmlNode node)
        {
            if (!string.IsNullOrEmpty(eventName)) return;
            eventName = node.InnerText;
        }
        private void setEventKeyByAttr(XpathItem ruleItem, HtmlNode node, string attrName)
        {
            if (!string.IsNullOrEmpty(eventKey)) return;
            if (!node.Attributes.Contains(attrName)) return;
            eventKey = node.Attributes[attrName].Value;
        }
        private void setEventKeyByAttr(XpathItem ruleItem, HtmlNode node, string attrName, string pattern, int groupIndex)
        {
            if (!string.IsNullOrEmpty(eventKey)) return;
            if (!node.Attributes.Contains(attrName)) return;

            string attrValue = node.Attributes[attrName].Value;
            attrValue = UtilWxg.GetMatchGroup(attrValue, pattern, groupIndex);
            if (Regex.IsMatch(attrValue, @"^\d*$")) return;

            eventKey = attrValue;
        }
        public string ToStringLine()
        {
            List<string> lstItem = new List<string>();
            lstItem.Add(No.ToString());
            lstItem.Add(title);
            lstItem.Add(subpath);
            lstItem.Add(eventText);
            lstItem.Add(caseDesc);
            lstItem.Add(condition);
            return string.Join("\t", lstItem);
        }
    }
}
