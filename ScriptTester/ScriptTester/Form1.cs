using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ScriptTester
{
    public partial class Form1 : Form
    {
        private int ticks = 0;

        //This scraper uses less strict regex ,and appends sections to domain instead(initialy ment for targeting single domain)
        public Form1()
        {
            InitializeComponent();
            this.labelDate.Text = DateTime.Now.ToLongDateString();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            backgroundWorker1.WorkerSupportsCancellation = true; //needs a stop btn to stop thread/method
            this.timerElapsed.Start();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //WebBrowser browser = new WebBrowser(textBoxInputUrl.Text);
            //browser.BrowseSite();
            //browser.Dispose(); old code

            RunSearch runSearch = new RunSearch(textBoxInputUrl.Text);
            runSearch.CrawlUrl();

            this.timerElapsed.Stop();
        }

        //private async void BrowseAsync()
        //{
        //    WebBrowser browser = new WebBrowser(textBoxInputUrl.Text);
        //    await browser.BrowseSite(); //TODO : make BrowseSite() return [siteScreenshots]...on stop click
        //}

        private void timerElapsed_Tick(object sender, EventArgs e)
        {
            ticks++;
            this.labelTimerElapsed.Text = "Elapsed time : " + ticks.ToString() + " sec";
        }
    }
}