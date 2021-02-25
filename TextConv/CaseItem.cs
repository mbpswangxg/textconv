using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace TextConv
{
    public class CaseItem
    {
        public string title;
        public string subpath;
        public string eventKey;
        public string eventName;
        public string eventText;
        public string caseDesc;
        public string condition;

        public void refresh(XpathItem ruleItem, HtmlNode node)
        {
            if (ruleItem.name.Contains("button"))
            {
                setEventNameByAttr(ruleItem, node, "value");
            }
            
            setEventNameByAttr(ruleItem, node, "id");
            setEventNameByAttr(ruleItem, node, "name");
            setEventNameByInnerText(ruleItem, node);
            setEventNameByAttr(ruleItem, node, "value");

            setEventKeyByAttr(ruleItem, node, "name");
            setEventKeyByAttr(ruleItem, node, "onclick", @"\w+\('(\w+)'", 1);
            setEventKeyByAttr(ruleItem, node, "onclick", @"(\w+)\(", 1);
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
            string av = node.Attributes[attrName].Value;
            eventKey = UtilWxg.GetMatchGroup(av, pattern, groupIndex);
        }
        public string ToStringLine()
        {
            List<string> lstItem = new List<string>();
            lstItem.Add(title);
            lstItem.Add(subpath);
            lstItem.Add(eventText);
            lstItem.Add(caseDesc);
            lstItem.Add(condition);
            return string.Join("\t", lstItem);
        }
    }
}
