using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextConv
{
    public class WebRunner : IDisposable
    {
        private IWebDriver driver;
        private IJavaScriptExecutor js;

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
                foreach (XActionItem item in webAction.actions)
                {
                    action = item;
                    index++;
                    if (item.beforeShot)
                    {
                        SendKeys.SendWait("%{PRTSC}"); //ctr:^, alt:%
                    }
                    webAction.doAction(item);
                    Console.WriteLine("step{0}: {1}", index, item.ToString());

                    if (item.afterShot)
                    {
                        SendKeys.SendWait("%{PRTSC}"); //ctr:^, alt:%
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if(action != null)
                {
                    Console.WriteLine(action.details());
                }
            }

        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
