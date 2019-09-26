using System.Collections.Generic;

namespace ScriptTester
{
    public class CrawledURL
    {
        public string Url { get; set; }
        public bool IsChecked { get; set; }
        public List<string> FoundKeywords { get; private set; }

        public CrawledURL(string url, bool isChecked)
        {
            this.Url = url;
            this.IsChecked = isChecked;
            this.FoundKeywords = new List<string>();
        }

        /// <summary>
        /// Checks if object contains keyword (if empty -->returns all items in list)
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public bool UrlContainsKeyword(List<string> keywords)
        {
            foreach (var kw in keywords)
            {
                if (this.FoundKeywords.Contains(kw))
                    return true;
            }

            return false;
        }
    }
}