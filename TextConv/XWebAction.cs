using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;

namespace TextConv
{
    public class StringPair
    {
        public string key;
        public string mark;
        public string value;
        public override string ToString()
        {
            return string.Format("key:{0}, mark:{1}, value:{2}", key, mark, value);
        }
    }

    public class XActionItem
    {
        public XWebAction parent;

        public string command;
        public string target;
        public string value;
        public string shotflag;

        public bool screenshot;
        public bool isTrue = true;
        public List<XActionItem> subActions = new List<XActionItem>();
        public Size size {
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
        public string nextStep
        {
            get {
                string gotoValue = UtilWxg.GetMatchGroup(target, @"goto[:\s]+(\w+)",1);
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
            get {
                if (string.IsNullOrEmpty(shotflag)) return false;
                return shotflag.Equals("1");
            }
        }
        public bool IsSkipShot
        {
            get { 
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
            for(int i =1; i<lines.Length; i++)
            {
                m = Regex.Match(lines[i], @"(\w+)\|([^\|]+)\|([^\|]+)");
                if (m.Success)
                {
                    XActionItem sItem = new XActionItem();
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
                    XActionItem sItem = new XActionItem();
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
                if (targetPair.mark.Equals("<") &&  v1 < v2)
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
        private StringPair getPair(string input)
        {
            if (IsPair(input))
            {
                Match m = Regex.Match(input, @"^(\w+)([!=\<\>])([^;]+)");
                StringPair pair = new StringPair();
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
    public class XWebAction
    {
        public string name;
        public int interval;
        public int shotfromstep;
        public List<XActionItem> actions = new List<XActionItem>();
        public List<string> shotcmd = new List<string>();
        
        public int nextStepIndex { get; private set; }
        
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
                item.Init();
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
            int retryIndex = 0;
            int maxRetry = int.Parse(Config.GetAppSettingValue2("web.error.retry", "3"));
            
            while (retryIndex < maxRetry)
            {
                try
                {
                    windowAction(action);
                    wait();
                    return true;
                }
                catch (WebDriverException ex)
                {
                    retryIndex++;
                    Console.WriteLine(ex.Message);
                    continue;
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return false;
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
            if (action.IsTarget("switchwindow"))
            {
                driver.SwitchTo().Window(vars[action.value].ToString());
                return true;
            }

            if (action.IsCmd("switchTo"))
            {
                if (action.IsTarget("window"))
                {
                    driver.SwitchTo().Window(vars[action.value].ToString());
                }
                if (action.IsTarget("Frame"))
                {
                    if (action.index != -1)
                    {
                        driver.SwitchTo().Frame(action.index);
                    }else
                    {
                        driver.SwitchTo().Frame(action.value);
                    }
                }
                else if (action.IsTarget("DefaultContent"))
                {
                    driver.SwitchTo().DefaultContent();
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
                    vars[action.value] = driver.WindowHandles;
                }else if (action.IsTarget("CurrentWindowHandle"))
                {
                    vars[action.value] = driver.CurrentWindowHandle;
                }
                return true;
            }
            // save WindowHandles
            if (action.IsCmd("close"))
            {
                driver.Close();
                return true;
            }
            // save WindowHandles
            if (action.IsCmd("sendkeys"))
            {
                if(string.IsNullOrEmpty(action.target) 
                || Regex.IsMatch(action.target, "window"))
                {
                    SendKeys.SendWait(action.value);
                    return true;
                }
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
            if (action.IsCmd("var"))
            {
                vars[action.target] = action.value;
                return true;
            }
            if (action.IsCmd("math"))
            {
                mathAction(action);
                return true;
            }

            if (action.IsCmd("ifvar"))
            {
                // if not equals, skip goto.
                action.isTrue =action.IsTargetValid(vars);
                return true;
            }
            return false;
        }
        private void mathAction(XActionItem action)
        {
            //Add substract
            Match m = Regex.Match(action.target, @"(\w+)([\+\-\*\/])(\d+)");
            if (m.Success)
            {
                string cmd = m.Groups[1].Value;
                string sign = m.Groups[2].Value;
                string val2 = m.Groups[3].Value;
                decimal var1 = 0;
                if (vars.ContainsKey(cmd))
                {
                    var1 = decimal.Parse(vars[cmd].ToString());
                }
                else
                {
                    vars.Add(cmd, var1);
                }

                if (sign.Equals("+"))
                {
                    var1 = var1 + decimal.Parse(val2);
                }
                else if (sign.Equals("-"))
                {
                    var1 = var1 - decimal.Parse(val2);
                }
                else if (sign.Equals("*"))
                {
                    var1 = var1 * decimal.Parse(val2);
                }
                else if (sign.Equals("/"))
                {
                    var1 = var1 / decimal.Parse(val2);
                }
                vars[cmd] = var1;
            }
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
            IWebElement element = TryFindElement(action);

            // if not found, goto
            if (action.IsCmd("ifnot"))
            {
                action.isTrue = (element == null);
                return true;
            }
            // if not found, goto
            if (action.IsCmd("ifind"))
            {
                action.isTrue = (element != null);
                return true;
            }

            if (element == null)
            {
                return false;
            }

            if (action.IsCmd("click"))
            {
                element.Click();
                return true;
            }
            if (action.IsCmd("popup"))
            {
                //POPUP画面起動前
                vars["WindowHandles"] = driver.WindowHandles;
                element.Click();
                wait();

                string windowId = string.Format("window{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
                //POPUP画面起動後
                vars[windowId] = waitForWindow(2000);
                //POPUP画面へ切替前、本画面を一時退避
                vars["root"] = driver.CurrentWindowHandle;
                //POPUP画面へ切替
                driver.SwitchTo().Window(vars[windowId].ToString());
                return true;
            }

            if (action.IsCmd("sendkeys") || action.IsCmd("type"))
            {
                element.SendKeys(action.value);
                return true;
            }else if (action.IsCmd("overwrite"))
            {
                element.Clear();
                element.SendKeys(action.value);
                return true;
            }
            return false;
        }
        
        private IWebElement TryFindElement(XActionItem action)
        {
            IWebElement element = null;
            int frameIndex = 0;
            IWebElement brotherFrame = null;
            IReadOnlyCollection<IWebElement> frameset = null;

            int retry = 0;

            // if not found in current page, check frame.
            while (retry < 2)
            {
                try
                {
                    element = FindElement(action);
                    return element;
                }
                catch(WebDriverException ex)
                {
                    bool isKeepAlive = true;
                    if(ex.InnerException != null)
                    {
                        System.Net.WebException wex = ex.InnerException as System.Net.WebException;
                        if(wex.Status == System.Net.WebExceptionStatus.KeepAliveFailure)
                        {
                            isKeepAlive = false;
                            retry++;
                        }
                    }
                    if (isKeepAlive)
                    {
                        if (brotherFrame == null)
                        {
                            frameIndex = 0;
                            frameset = driver.FindElements(By.XPath("//frame"));
                            if (frameset.Count > 0)
                            {
                                driver.SwitchTo().Frame(frameIndex);
                                brotherFrame = frameset.ToArray()[frameIndex];
                                frameIndex++;
                            }
                            else
                            {
                                if (!action.IsCmd("ifnot") &&  !action.IsCmd("ifind"))
                                {
                                    Console.WriteLine("★Error on {0}\n{1}", action.ToString(), ex.Message);
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                        else if (frameset.Count > frameIndex)
                        {
                            driver.SwitchTo().ParentFrame();
                            driver.SwitchTo().Frame(frameIndex);
                            brotherFrame = frameset.ToArray()[frameIndex];
                            frameIndex++;
                        }
                        else
                        {
                            brotherFrame = null;
                        }
                        retry++;
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return null;
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
            if (action.IsTargetKey("linktext"))
            {
                element = driver.FindElement(By.LinkText(action.targetPair.value));
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
