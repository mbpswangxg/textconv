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
        public string title;
        public string subpath;
        public string eventKey;
        public string eventName;
        public string eventText;
        public string caseDesc;
        public string condition;

        private string attrname;
        private static string ptwords = @"\>([\w\s]+)";

        public void refresh(XPathRuleItem ruleItem, HtmlNode node)
        {
            setEventKeyByAttr(ruleItem, node);
            setEventNameByAttr(ruleItem, node);

            if (string.IsNullOrEmpty(eventName) || !Regex.IsMatch(eventName, @"\w+"))
            {
                if (!string.IsNullOrEmpty(eventKey))
                {
                    if (ruleItem.wordMap.ContainsKey(this.eventKey))
                    {
                        this.eventName = ruleItem.wordMap[this.eventKey];
                    }
                    else
                    {
                        eventName = eventKey;
                    }
                }
            }
            else if (Regex.IsMatch(this.eventName, ptwords))
            {
                this.eventName = UtilWxg.GetMatchGroup(this.eventName, ptwords, 1);
            }
            
            if (ruleItem.wordMap.ContainsKey(this.eventName))
            {
                this.eventName = ruleItem.wordMap[this.eventName];
            }
        }

        public void ToCaseDesc(XPathRuleItem ruleItem, HtmlNode node)
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
            
            if (ruleItem.name.Contains("calender"))
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
            if (ruleItem.caseMap.ContainsKey(this.eventName))
            {
                this.eventText = ruleItem.caseMap[this.eventName];
            }

            caseDesc = UtilWxg.ReplaceKeyValue(ruleItem.caseDescFormat, "eventname", eventName);
        }

        private void setEventKeyByAttr(XPathRuleItem rule, HtmlNode node)
        {
            if (!string.IsNullOrEmpty(eventKey)) return;
            
            foreach (var k in rule.keypattern)
            {
                if (!node.Attributes.Contains(k.attrname)) continue;
                attrname = k.attrname;
                string attrValue = node.Attributes[k.attrname].Value;
                if (Regex.IsMatch(attrValue, @"^[\w\.]+$"))
                {
                    eventKey = attrValue;
                }
                else if (Regex.IsMatch(attrValue, k.pattern))
                {
                    eventKey = Regex.Replace(attrValue, k.pattern, k.replacement);
                }
                
                if (!string.IsNullOrEmpty(eventKey)) return;
            }
        }
        private void setEventNameByAttr(XPathRuleItem rule, HtmlNode node)
        {
            if (!string.IsNullOrEmpty(eventName)) return;

            HtmlDocument htmlDoc = node.OwnerDocument;
            foreach (var xpath in rule.textxpath)
            {
                // error case 
                if (string.IsNullOrEmpty(eventKey)) continue;
                
                string xpath2 = string.Format(xpath, attrname, eventKey);
                var n2 = htmlDoc.DocumentNode.SelectNodes(xpath2);
                if (n2 != null)
                {
                    HtmlNode firstNode = n2.First();
                    string innerText = firstNode.InnerText.Trim();
                    if (Regex.IsMatch(innerText,@"\w+"))
                    {
                        this.eventName = WebUtility.HtmlDecode(innerText);
                        break;
                    }
                }
            }
        }

        public string ToStringLine()
        {
            List<string> lstItem = new List<string>();
            lstItem.Add(title);
            lstItem.Add(subpath);
            //lstItem.Add(eventKey);
            //lstItem.Add(eventName);
            lstItem.Add(eventText);
            lstItem.Add(caseDesc);
            lstItem.Add(condition);
            return string.Join("\t", lstItem);
        }
    }
}
