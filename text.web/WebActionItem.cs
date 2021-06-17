using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using Text.Common;

namespace Text.Web
{
    public class WebActionItem
    {
        public WebAction parent;

        public string command;
        public string target;
        public string value;
        public string shotflag;

        public bool screenshot;
        public bool isTrue = true;
        public List<WebActionItem> subActions = new List<WebActionItem>();
        public Size size
        {
            get
            {
                if (string.IsNullOrEmpty(value)) return Size.Empty;

                Match m = Regex.Match(value, @"^([\d\s]+)[,x:]([\d\s]+)$");
                if (m.Success)
                {
                    int width = int.Parse(m.Groups[1].Value);
                    int height = int.Parse(m.Groups[2].Value);
                    return new Size(width, height);
                }
                return new Size(-1, -1);
            }
        }
        public int index
        {
            get
            {
                if (string.IsNullOrEmpty(value)) return -1;
                if (!Regex.IsMatch(value, @"\d+")) return -1;
                return int.Parse(value);
            }
        }
        public WebParam targetPair
        {
            get
            {
                return getPair(target);
            }
        }
        public WebParam valuePair
        {
            get
            {
                return getPair(value);
            }
        }
        public bool IsTargetPair
        {
            get { return IsPair(target); }
        }
        public bool IsValuePair
        {
            get { return IsPair(value); }
        }
        public string nextStep
        {
            get
            {
                string gotoValue = StringUtils.GetMatchGroup(target, @"goto[:\s]+(\w+)", 1);
                if (string.IsNullOrEmpty(gotoValue) && IsCmd("goto"))
                {
                    gotoValue = target;
                }
                return gotoValue;
            }
        }
        public bool IsCmd(string name)
        {
            return command.Equals(name, StringComparison.OrdinalIgnoreCase);
        }
        public bool IsTarget(string name)
        {
            if (string.IsNullOrEmpty(target)) return false;
            return target.Equals(name, StringComparison.OrdinalIgnoreCase);
        }
        public bool IsTargetKey(string name)
        {
            if (!IsTargetPair) return false;
            return targetPair.key.Equals(name, StringComparison.OrdinalIgnoreCase);
        }
        public bool IsValueKey(string name)
        {
            if (!IsValuePair) return false;
            return valuePair.key.Equals(name, StringComparison.OrdinalIgnoreCase);
        }
        public bool IsForceShot
        {
            get
            {
                if (string.IsNullOrEmpty(shotflag)) return false;
                return shotflag.Equals("1");
            }
        }
        public bool IsSkipShot
        {
            get
            {
                if (string.IsNullOrEmpty(shotflag)) return false;
                return shotflag.Equals("-1");
            }
        }

        public override string ToString()
        {
            return string.Format("command={0} | target={1} | value={2}", command, target, value);
        }
        public void Init()
        {
            if (string.IsNullOrEmpty(target)) return;
            string[] lines = Regex.Split(target, @";\s*");
            Match m;
            for (int i = 1; i < lines.Length; i++)
            {
                m = Regex.Match(lines[i], @"(\w+)\|([^\|]+)\|([^\|]+)");
                if (m.Success)
                {
                    WebActionItem sItem = new WebActionItem();
                    sItem.command = m.Groups[1].Value;
                    sItem.target = m.Groups[2].Value;
                    sItem.value = m.Groups[3].Value;
                    this.subActions.Add(sItem);
                    sItem.isTrue = this.isTrue;
                    sItem.shotflag = this.shotflag;
                    sItem.parent = this.parent;
                    continue;
                }
                m = Regex.Match(lines[i], @"(\w+)\|([^\|]+)");
                if (m.Success)
                {
                    WebActionItem sItem = new WebActionItem();
                    sItem.command = m.Groups[1].Value;
                    sItem.target = m.Groups[2].Value;
                    this.subActions.Add(sItem);
                    sItem.isTrue = this.isTrue;
                    sItem.shotflag = this.shotflag;
                    sItem.parent = this.parent;
                    continue;
                }
            }
        }
        public bool isShotCmd
        {
            get
            {
                if (parent.shotcmd.Contains(command))
                {
                    return true;
                }
                if (IsCmd("switchTo"))
                {
                    string cmd = "switch" + target;
                    if (parent.shotcmd.Contains(cmd))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public bool IsTargetValid(IDictionary<string, object> vals)
        {
            if (!IsTargetPair) return false;
            if (string.IsNullOrEmpty(targetPair.mark)) return false;
            if (string.IsNullOrEmpty(targetPair.value)) return false;
            if (!vals.ContainsKey(targetPair.key)) return false;

            Console.WriteLine("◆val[{0}]={1}, targetPair={2}", targetPair.key, vals[targetPair.key], targetPair.ToString());

            if (Regex.IsMatch(targetPair.mark, @"^=$"))
            {
                if (vals[targetPair.key].Equals(targetPair.value))
                {
                    return true;
                }
                if (vals[targetPair.key].ToString().Equals(targetPair.value))
                {
                    return true;
                }
            }
            else if (Regex.IsMatch(targetPair.mark, @"^[\<\>]$"))
            {
                decimal v1 = decimal.Parse(vals[targetPair.key].ToString());
                decimal v2 = decimal.Parse(targetPair.value);
                if (targetPair.mark.Equals("<") && v1 < v2)
                {
                    return true;
                }
                else if (targetPair.mark.Equals(">") && v1 > v2)
                {
                    return true;
                }
            }

            return false;
        }
        #region private methods
        private WebParam getPair(string input)
        {
            if (IsPair(input))
            {
                Match m = Regex.Match(input, @"^(\w+)([!=\<\>])([^;]+)");
                WebParam pair = new WebParam();
                pair.key = m.Groups[1].Value;
                pair.mark = m.Groups[2].Value;
                pair.value = m.Groups[3].Value;
                return pair;
            }
            return null;
        }
        private bool IsPair(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            return Regex.IsMatch(input, @"^(\w+)=(.+)$");
        }
        #endregion
    }
}
