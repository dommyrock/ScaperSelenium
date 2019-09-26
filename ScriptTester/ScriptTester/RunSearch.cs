using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScriptTester
{
    public class RunSearch
    {
        public bool IsActive { get; set; } = true;

        public int CurrentUrlIndex { get; private set; } = 0;

        public string CurrentUrlString { get; set; }

        public string InputUrl { get; set; }

        public string Result { get; set; }

        public RunSearch RunSearchContext { get; set; }

        public List<string> InputUrls;

        public Dictionary<string, CrawledURL> CheckedUrlsDictionary;

        public RunSearch(string input)
        {
            this.InputUrl = input;
            this.InputUrls = new List<string>();
            this.CheckedUrlsDictionary = new Dictionary<string, CrawledURL>();
            this.InputUrls.Add(input);
        }

        public void CrawlUrl()
        {
            while (this.IsActive)
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
                                this.CheckedUrlsDictionary.Add(urlObject.Url, urlObject); //add checked url to list of checked urls
                            }
                        }

                        #region WebException

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

                #region Regex

                string data = String.Join(" ", this.Result); // has to be joined --> cant transform directly from list to string ,(returns just name of class to string)
                Regex urlRegex = new Regex(@"(?<=\bhref="")[^""]*", RegexOptions.IgnoreCase);
                MatchCollection matches = urlRegex.Matches(data);

                foreach (Match match in matches)
                {
                    this.InputUrls.Add(match.Value); //insert bellow 1st Url ...
                }

                #endregion Regex

                this.InputUrls.RemoveAt(this.CurrentUrlIndex);
            }
        }

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
    }
}