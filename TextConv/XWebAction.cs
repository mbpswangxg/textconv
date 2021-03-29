using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace TextConv
{
    public class StringPair
    {
        public string key;
        public string value;
    }

    public class XActionItem
    {
        public XWebAction parent;

        public string command;
        public string target;
        public string value;
        public bool screenshot;

        public Size size {
            get
            {
                if (string.IsNullOrEmpty(value)) return Size.Empty;

                Match m = Regex.Match(value, @"^([\d\s]+),([\d\s]+)$");
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
        public StringPair targetPair
        {
            get
            {
                return getPair(target);
            }
        }
        public StringPair valuePair
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

        public bool IsCmd(string name)
        {
            return command.Equals(name, StringComparison.OrdinalIgnoreCase);
        }
        public bool IsTarget(string name)
        {
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

        public string details()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\tcommand:").AppendLine(command);
            sb.Append("\ttarget:").AppendLine(target);
            sb.Append("\tvalue:").AppendLine(value);
            return sb.ToString();
        }
        public bool beforeShot
        {
            get
            {
                if (parent.shotcmd_before.Contains(command))
                {
                    return true;
                }
                return false;
            }
            
        }
        public bool afterShot 
        {
            get
            {
                if (parent.shotcmd_after.Contains(command))
                {
                    return true;
                }
                return false;
            }
        }
        #region private methods
        private StringPair getPair(string input)
        {
            if (IsPair(input))
            {
                Match m = Regex.Match(input, @"^(\w+)=(.+)$");
                StringPair pair = new StringPair();
                pair.key = m.Groups[1].Value;
                pair.value = m.Groups[2].Value;
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
    public class XWebAction
    {
        public string name;
        public int interval;
        public List<XActionItem> actions = new List<XActionItem>();
        public List<string> shotcmd_before = new List<string>();
        public List<string> shotcmd_after = new List<string>();

        public IDictionary<string, object> vars { get; private set; }
        public IWebDriver driver;
        public XWebAction()
        {
            vars = new Dictionary<string, object>();
        }
        public void Init()
        {
            foreach (XActionItem item in actions)
            {
                item.parent = this;
            }
        }
        public void wait()
        {
            wait(interval);
        }
        public void wait(int timeout)
        {
            try
            {
                Thread.Sleep(timeout);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
        }

        public bool doAction(XActionItem action)
        {
            try
            {
                windowAction(action);
                wait();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(action.details());
                return false;
            }
        }
        private bool windowAction(XActionItem action)
        {
            // open url
            if (action.IsCmd("open"))
            {
                driver.Navigate().GoToUrl(action.target);
                return true;
            }

            // switch to dest window or frame
            if (action.IsCmd("switchTo"))
            {
                if (action.IsTarget("window"))
                {
                    driver.SwitchTo().Window(vars[action.value].ToString());
                }
                else if (action.IsTarget("Frame"))
                {
                    if (action.index != -1)
                    {
                        driver.SwitchTo().Frame(action.index);
                    }else
                    {
                        driver.SwitchTo().Frame(action.value);
                    }
                }
                return true;
            }

            // resize, wait or others
            if (otherAction(action))return true;

            // find the element, click or sendkeys
            if (elementAction(action)) return true;

            return false;
        }
        private bool otherAction(XActionItem action)
        {
            // resize window
            if (action.IsCmd("resize"))
            {
                if (action.IsTarget("window"))
                {
                    driver.Manage().Window.Size = action.size;
                }
                return true;
            }
            // wait interval ms.
            if (action.IsCmd("wait"))
            {
                if (string.IsNullOrEmpty(action.value))
                {
                    wait();
                }
                else
                {
                    wait(int.Parse(action.value));
                }
                return true;
            }
            // save WindowHandles
            if (action.IsCmd("save"))
            {
                if (action.IsTarget("WindowHandles"))
                {
                    vars["WindowHandles"] = driver.WindowHandles;
                }
                return true;
            }
            // save WindowHandles
            if (action.IsCmd("waitForWindow"))
            {
                if (!string.IsNullOrEmpty(action.target))
                {
                    vars[action.target] = waitForWindow(int.Parse(action.value));
                }
                return true;
            }
            return false;
        }
        private string waitForWindow(int timeout)
        {
            try
            {
                Thread.Sleep(timeout);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
            var whNow = ((IReadOnlyCollection<object>)driver.WindowHandles).ToList();
            var whThen = ((IReadOnlyCollection<object>)vars["WindowHandles"]).ToList();
            if (whNow.Count > whThen.Count)
            {
                return whNow.Except(whThen).First().ToString();
            }
            else
            {
                return whNow.First().ToString();
            }
        }
        
        private bool elementAction(XActionItem action)
        {
            IWebElement element = FindElement(action);

            if (element == null)
            {
                Console.Write("Can't found element. action details:\n{0}", action.details());
                return false;
            }
            
            if (action.IsCmd("click"))
            {
                element.Click();
                return true;
            }
            if (action.IsCmd("sendkeys"))
            {
                element.SendKeys(action.value);
                return true;
            }
            return false;
        }
        private IWebElement FindElement(XActionItem action)
        {
            IWebElement element = null;
            if (action.IsTargetKey("css"))
            {
                element = driver.FindElement(By.CssSelector(action.targetPair.value));
            }
            if (action.IsTargetKey("id"))
            {
                element = driver.FindElement(By.Id(action.targetPair.value));
            }
            if (action.IsTargetKey("name"))
            {
                element = driver.FindElement(By.Name(action.targetPair.value));
            }
            if (action.IsTargetKey("XPath"))
            {
                if (string.IsNullOrEmpty(action.value))
                {
                    element = driver.FindElement(By.XPath(action.targetPair.value));
                }
                else if (action.IsValueKey("parentname"))
                {
                    var parent = driver.FindElement(By.Name(action.valuePair.value));
                    element = parent.FindElement(By.XPath(action.targetPair.value));
                }
            }
            else if (action.IsTargetKey("XPathFormat")) 
            {
                string xpath = string.Empty;
                if (action.IsValuePair)
                {
                    xpath = string.Format(action.targetPair.value, action.valuePair.value);
                }
                else
                {
                    xpath = string.Format(action.targetPair.value, action.value);
                }
                if (!string.IsNullOrEmpty(xpath))
                {
                    element = driver.FindElement(By.XPath(xpath));
                }
            }

            return element;
        }
    }

}
