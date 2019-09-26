using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptTester
{
    public class WebBrowser : RunSearch
    {
        private IWebDriver WebDriver;

        private ChromeDriverService ChromeDriverService;
        public int VslidLinksParsed { get; set; }

        public WebBrowser(string input) : base(input) //need to pass input params to both ,because their constructors demand it !
        {
            this.RunSearchContext = new RunSearch(input);
            this.RunSearchContext.InputUrl = input;
            this.RunSearchContext.InputUrls = base.InputUrls;
            this.RunSearchContext.CheckedUrlsDictionary = base.CheckedUrlsDictionary;

            this.ChromeDriverService = ChromeDriverService.CreateDefaultService();
            //this.ChromeDriverService.HideCommandPromptWindow = true;

            if (!this.ChromeDriverService.IsRunning)
            {
                this.ChromeDriverService.Start();
            }
            else
            {
                Dispose();
                this.ChromeDriverService.Start();
            }

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            this.WebDriver = new ChromeDriver(this.ChromeDriverService, options);
            ServicePointManager.DefaultConnectionLimit = 20;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        //call RunSearch ---> crawlUrl here
        public void BrowseSite()
        {
            try
            {
                //call crawl url method here from runSearch context
                this.RunSearchContext.CrawlUrl();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #region Firefox

            //using (var firefoxDriver = new FirefoxDriver())
            //{
            //    this.Driver = firefoxDriver;
            //    firefoxDriver.Navigate().GoToUrl("http://www.google.com/");

            //    IWebElement querryElement = firefoxDriver.FindElement(By.Name("q"));

            //    //type someting in search box
            //    querryElement.SendKeys("NOA smarthone rewiews");
            //    querryElement.Submit();

            //    //wait

            //    Thread.Sleep(10000);
            //    //var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(10000));
            //    //wait.Until(w => w.Title.StartsWith("NOA"));
            //}

            #endregion Firefox
        }

        public void Dispose()
        {
            if (this.WebDriver != null)
            {
                this.WebDriver.Close();
                this.WebDriver.Quit();
                this.WebDriver.Dispose();
                this.WebDriver = null;
            }

            if (this.ChromeDriverService != null)
            {
                this.ChromeDriverService.Dispose();
                this.ChromeDriverService = null;
            }
        }
    }
}