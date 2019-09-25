using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ScriptTester
{
    public partial class Form1 : Form
    {
        private int ticks = 0;

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
            //RunSearch runSearch = new RunSearch(textBoxInputUrl.Text);
            ////runSearch.HtmlCrawl();

            //runSearch.CrawlUrl();
            //this.timerElapsed.Stop();

            //TODO
            //this calls RunSerarch in it's class
            WebBrowser browser = new WebBrowser(textBoxInputUrl.Text);
            browser.BrowseSite();
        }

        private void timerElapsed_Tick(object sender, EventArgs e)
        {
            ticks++;
            this.labelTimerElapsed.Text = "Elapsed time : " + ticks.ToString() + " sec";
        }
    }
}