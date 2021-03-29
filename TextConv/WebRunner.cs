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
            if (browerName.Equals("IE"))
            {
                driver = new InternetExplorerDriver();
            }
            else if (browerName.Equals("Chrome"))
            {
                driver = new ChromeDriver();
            }
            js = (IJavaScriptExecutor)driver;
        }

        public void Run(string cmd)
        {
            List<XWebAction> rules = new List<XWebAction>();
            string ymlPath = Config.GetAppSettingValue("web.rule.yml");
            YmlLoader.Load(rules, ymlPath, cmd);
            rules.ForEach(item => doAction(item));
        }
        
        private void doAction(XWebAction webAction)
        {
            webAction.driver = this.driver;

            int index = 0;
            foreach (XActionItem item in webAction.actions)
            {
                if (item.beforeShot)
                {
                    SendKeys.SendWait("%{PRTSC}"); //ctr:^, alt:%
                }
                bool isOK = webAction.doAction(item);
                if (item.afterShot && isOK)
                {
                    SendKeys.SendWait("%{PRTSC}"); //ctr:^, alt:%
                }
                index++;
            }
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
