using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TextConv
{
    public class WebRunner : IDisposable
    {
        private IWebDriver driver;
        private IJavaScriptExecutor js;
        public int ErrorCount = 0;
        public WebRunner()
        {
            string browerName = Config.GetAppSettingValue("web.browser");
            if (browerName.Equals("IE", StringComparison.OrdinalIgnoreCase))
            {
                driver = new InternetExplorerDriver();
            }
            else if (browerName.Equals("Chrome", StringComparison.OrdinalIgnoreCase))
            {
                driver = new ChromeDriver();
            }
            js = (IJavaScriptExecutor)driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void Run(string ymlFilePath)
        {
            List<XWebAction> rules = new List<XWebAction>();
            YmlLoader.LoadFromFile(rules, ymlFilePath);
            rules.ForEach(item => doAction(item));
        }
        public void RunBatch(string ymlDirPath)
        {
            List<XWebAction> rules = new List<XWebAction>();
            YmlLoader.Load(rules, ymlDirPath);
            rules.ForEach(item => doAction(item));
        }

        private void doAction(XWebAction webAction)
        {
            webAction.driver = this.driver;
            XActionItem action = null;
            try
            {
                int index = 0;
                for(int i =0; i< webAction.actions.Count; i++)
                {
                    index = i + 1;
                    action = webAction.actions[i];
                    if (action.beforeShot && webAction.shotfromstep < index)
                    {
                        SendKeys.SendWait("%{PRTSC}"); //ctr:^, alt:%
                    }
                    
                    Console.WriteLine("step{0:D3}: {1}", index, action.ToString());
                    if (!webAction.doAction(action))
                    {
                        Console.WriteLine("★★★異常発生した為、処理中止...★★★");
                        break;
                    }
                    if (action.IsForceShot)
                    {
                        SendKeys.SendWait("%{PRTSC}"); //ctr:^, alt:%
                    }
                    else if (!action.IsSkipShot) 
                    {
                        if (action.afterShot && webAction.shotfromstep <= index && !webAction.shotskip.Contains(index))
                        {
                            SendKeys.SendWait("%{PRTSC}"); //ctr:^, alt:%
                        }
                    }

                    if(!string.IsNullOrEmpty(action.nextStep) && action.jump)
                    {
                        //default goto stepindex. ifind=true or ifnot=false or ifvar=true
                        i = webAction.actions.FindIndex(ac => ac.command.Equals("label") && ac.target.Equals(action.nextStep)) - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if(action != null)
                {
                    Console.WriteLine(action.ToString());
                }
            }

        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
