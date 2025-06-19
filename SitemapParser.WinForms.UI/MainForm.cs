using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ParserLogic;
using SitemapParser.WInForms.UI.Helpers;

namespace SitemapParser.WInForms.UI
{
    public partial class MainForm : Form
    {
        private SitemapService _service;

        public MainForm()
        {
            InitializeComponent();
            _service = new SitemapService();

           // tbUrl.Text = "https://local.staging.ritehite.com/ritehitesitemap/sitemap.am.xml";
            tbUrl.Text = "https://www.ritehite.com/ritehitesitemap/sitemap.am.xml";
        }


        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse the sitemap and get URLs
                var xmlFilePath = tbUrl.Text;
                var urls = _service.DownloadAndParse(xmlFilePath).Result;

                // Save URLs to CSV
                var csvFilePath = FormsHelper.GetOutputFilePath();
                _service.SaveToCsv(urls, csvFilePath);

                //Console.WriteLine($"Successfully extracted {urls.Count} URLs to {csvFilePath}");
            }
            catch (Exception ex)
            {
                //Console.WriteLine($@"Error: {ex.Message}");
            }
        }


    }
}
