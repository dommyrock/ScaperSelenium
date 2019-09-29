using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ScriptTester
{
    public class RunSearch : WebBrowser
    {
        public bool IsActive { get; set; } = true;
        public string InputUrl { get; set; }
        private int CurrentUrlIndex { get; set; } = 0;
        private string CurrentUrlString { get; set; }
        private string Result { get; set; }

        public List<string> InputUrls;

        public Dictionary<string, CrawledURL> CheckedUrlsDictionary;

        public List<Screenshot> TempFileStore { get; set; }

        public ChromeDriver C_Driver;

        public RunSearch(string input) : base(input)//pass inout to bas class constructor
        {
            this.InputUrl = input;
            this.InputUrls = new List<string>();
            this.CheckedUrlsDictionary = new Dictionary<string, CrawledURL>();
            this.InputUrls.Add(input);
            this.C_Driver = base.CDriver;
        }

        //Pass params to -----> WebBrowser Class (instance )
        public void CrawlUrl()
        {
            while (this.IsActive)
            {
                try
                {
                    this.CurrentUrlString = this.InputUrls[this.CurrentUrlIndex];//could dirrectly replace currentURL with this.currenturlstring to simpliy...

                    if (ValidateUri(this.CurrentUrlString)) //calls Uri validation method
                    {
                        if (!this.CheckedUrlsDictionary.ContainsKey(this.CurrentUrlString))
                        {
                            try
                            {
                                CrawledURL urlObject = new CrawledURL(this.CurrentUrlString, false);
                                urlObject.Url = this.CurrentUrlString;

                                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.CurrentUrlString);
                                request.ContentType = "text/html";
                                request.Accept = "txt/html";
                                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                                {
                                    using (Stream st = response.GetResponseStream())
                                    using (StreamReader reader = new StreamReader(st))
                                    {
                                        this.Result = reader.ReadToEnd();
                                    }

                                    urlObject.IsChecked = true;
                                    //Add checked url to list of checked urls
                                    this.CheckedUrlsDictionary.Add(urlObject.Url, urlObject);

                                    //Take Screenshot (from inherited(base) WebBrowser instance)
                                    base.TakeScreenshot(urlObject);

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
                            }

                            #region WebException

                            //currently ...skip link if it has exception
                            catch (WebException wex)
                            {
                                //HttpWebResponse responseException = (HttpWebResponse)wex.Response;
                                //if (responseException == null)
                                //{
                                //    this.InputUrls.RemoveAt(this.CurrentUrlIndex);
                                //    continue;
                                //}
                                //if (responseException.StatusCode == HttpStatusCode.NotFound)//http 404
                                //{
                                //    this.InputUrls.RemoveAt(this.CurrentUrlIndex);
                                //    continue;
                                //}
                                //else if (responseException.StatusCode.ToString() == "999")//custom handler za linkedin(custom exception)
                                //{
                                //    this.InputUrls.RemoveAt(this.CurrentUrlIndex);
                                //    continue;
                                //}
                                //else if (responseException.StatusCode == HttpStatusCode.RequestTimeout)//http 408
                                //{
                                //    this.InputUrls.RemoveAt(this.CurrentUrlIndex);
                                //    continue;
                                //}
                                //else if (responseException.StatusCode == HttpStatusCode.Forbidden)//403
                                //{
                                //    this.InputUrls.RemoveAt(this.CurrentUrlIndex);
                                //    continue;
                                //}
                                //else if (responseException.StatusCode == HttpStatusCode.NotAcceptable)//406
                                //{
                                //    this.InputUrls.RemoveAt(this.CurrentUrlIndex);
                                //    continue;
                                //}
                                //else if (responseException.StatusCode == HttpStatusCode.InternalServerError)//500
                                //{
                                //    this.InputUrls.RemoveAt(this.CurrentUrlIndex);
                                //    continue;
                                //}
                                //else if (responseException.StatusCode == HttpStatusCode.ServiceUnavailable)//503
                                //{
                                //    this.InputUrls.RemoveAt(this.CurrentUrlIndex);
                                //    continue;
                                //}
                                //else if (responseException.StatusCode == HttpStatusCode.Gone)
                                //{
                                //    this.InputUrls.RemoveAt(this.CurrentUrlIndex);
                                //    continue;
                                //}
                                //else if (responseException.StatusCode == HttpStatusCode.BadGateway)
                                //{
                                //    this.InputUrls.RemoveAt(this.CurrentUrlIndex);
                                //    continue;
                                //}
                                //throw wex;
                                this.InputUrls.RemoveAt(this.CurrentUrlIndex);
                                continue; //continues if it gets exception that is not handled
                            }

                            #endregion WebException
                        }
                        else
                        {
                            this.InputUrls.RemoveAt(this.CurrentUrlIndex);
                            continue;
                        }
                    }
                    else
                    {
                        this.InputUrls.RemoveAt(this.CurrentUrlIndex);
                        continue;
                    }

                    MatchAllLinks();
                    this.InputUrls.RemoveAt(this.CurrentUrlIndex);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    throw ex;
                }
            }
            base.Close();
            return;
        }

        #region RegexMethod

        private void MatchAllLinks()
        {
            string data = String.Join(" ", this.Result); // has to be joined --> cant transform directly from list to string ,(returns class Name.ToString)
            Regex urlRegex = new Regex(@"(?<=\bhref="")[^""]*", RegexOptions.IgnoreCase);
            MatchCollection matches = urlRegex.Matches(data);

            foreach (Match match in matches)
            {
                this.InputUrls.Add(match.Value); //insert bellow 1st Url ...
            }
        }

        #endregion RegexMethod

        #region URLValidation

        private bool ValidateUri(string uriString)
        {
            //Check if uri starts with / , if so ,prepend searched site to relative uri
            if (uriString.StartsWith("/") && !uriString.EndsWith("ico") && !uriString.EndsWith("png") && !uriString.EndsWith("xml"))
            {
                this.CurrentUrlString = this.InputUrl + uriString;
            }
            //check only sections of current domainv(can ignore for testing)
            if (this.CurrentUrlString.StartsWith(this.InputUrl))
            {
                Uri absUr;
                if (Uri.TryCreate(this.CurrentUrlString, UriKind.Absolute, out absUr) && (absUr.Scheme == Uri.UriSchemeHttp || absUr.Scheme == Uri.UriSchemeHttps))
                {
                    absUr = new Uri(this.CurrentUrlString);

                    this.CurrentUrlString = absUr.AbsoluteUri;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        #endregion URLValidation

        #region TestWithHtmlAgilityPackLib

        //Test with htmlParser -->htmlAgility library (not used anywhere atm)
        private string hardCodeUrl = "https://www.24sata.hr/";

        private List<HtmlNode> nodes = new List<HtmlNode>();

        private List<string> HtmlCrawl()
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument document = new HtmlDocument();
            document = htmlWeb.Load(hardCodeUrl);

            //HtmlNodeCollection body = document.DocumentNode.SelectNodes("//a[@href]");
            foreach (HtmlNode link in document.DocumentNode.SelectNodes("//a[@href]"))
            {
                string stringLink = link.GetAttributeValue("href", string.Empty);
                this.InputUrls.Add(stringLink);
            }

            return this.InputUrls; //just for test we add results here
        }

        #endregion TestWithHtmlAgilityPackLib

        //SAMPLE CODE
        //        public ISet<string> GetNewLinks(string content)
        //{
        //    Regex regexLink = new Regex("(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");

        //    ISet<string> newLinks = new HashSet<string>();
        //    foreach (var match in regexLink.Matches(content))
        //    {
        //        if (!newLinks.Contains(match.ToString()))
        //            newLinks.Add(match.ToString());
        //    }

        //    return newLinks;
        //}
    }
}