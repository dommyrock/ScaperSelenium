using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;
using System.Net;

namespace ScriptTester
{
    public class WebBrowser : IWebDriver
    {
        public IWebDriver WebDriver;

        public ChromeDriverService CDS { get; set; }
        public ChromeDriver CDriver { get; set; }
        public string Input { get; set; }

        public string Url { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Title => throw new NotImplementedException();
        public string PageSource => throw new NotImplementedException();
        public string CurrentWindowHandle => throw new NotImplementedException();
        public ReadOnlyCollection<string> WindowHandles => throw new NotImplementedException();

        //need to pass input params to both ,because their constructors demand it !
        public WebBrowser(string input)
        {
            this.Input = input;
            this.CDriver = new ChromeDriver();
        }

        #region OldCode

        //need to pass input params to both ,because their constructors demand it !
        //public WebBrowser(string input) : base(input)
        //{
        //    this.RunSearchContext = new RunSearch(input);
        //    this.RunSearchContext.InputUrl = input;
        //    this.RunSearchContext.InputUrls = base.InputUrls;
        //    this.RunSearchContext.CheckedUrlsDictionary = base.CheckedUrlsDictionary;

        //    this.ChromeDriverService = ChromeDriverService.CreateDefaultService();
        //    //this.ChromeDriverService.HideCommandPromptWindow = true;

        //    if (!this.ChromeDriverService.IsRunning)
        //    {
        //        ChromeOptions options = new ChromeOptions();
        //        options.AddArgument("--headless");
        //        //this.WebDriver = new ChromeDriver(this.ChromeDriverService, options); isnt used atm ...we initialize it in runSearch class
        //        ServicePointManager.DefaultConnectionLimit = 20;
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

        //        this.ChromeDriverService.Start();
        //    }
        //    else
        //    {
        //        Close();
        //    }
        //}

        #endregion OldCode

        public void TakeScreenshot(CrawledURL urlObject)
        {
            ////Store it as ....
            //string asString = screenshot.AsBase64EncodedString;
            //byte[] asByteArray = screenshot.AsByteArray;
            //string stringShot = screenshot.ToString();
            CDriver.Navigate().GoToUrl(urlObject.Url);

            Screenshot screenshot = ((ITakesScreenshot)CDriver).GetScreenshot();
            string friendlyDatetime = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            screenshot.SaveAsFile(@"E:\Output\" + friendlyDatetime + ".png", ScreenshotImageFormat.Png);//Make sure local dir exists !!!
        }

        public void Close()
        {
            if (this.CDriver != null)
            {
                //CDriver.Close();
                //CDriver.Dispose();
                CDriver.Quit();
                CDriver = null;
            }

            if (this.CDS != null)
            {
                CDS.Dispose();
                CDS = null;
            }
        }

        #region IWebDriverMethods

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IWebElement FindElement(By by)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            throw new NotImplementedException();
        }

        public IOptions Manage()
        {
            throw new NotImplementedException();
        }

        public INavigation Navigate()
        {
            throw new NotImplementedException();
        }

        public void Quit()
        {
            throw new NotImplementedException();
        }

        public ITargetLocator SwitchTo()
        {
            throw new NotImplementedException();
        }

        #endregion IWebDriverMethods
    }
}