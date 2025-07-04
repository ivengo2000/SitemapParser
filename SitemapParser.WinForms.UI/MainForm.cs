using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ParserLogic;
using SitemapParser.WInForms.UI.Helpers;
using SitemapParser.WInForms.UI.Properties;

namespace SitemapParser.WInForms.UI
{
    public partial class MainForm : Form
    {
        private readonly SitemapService _service;
        public MainForm()
        {
            InitializeComponent(); 
            _service = new SitemapService();
            InitializeComboBox(); 
        }
        private void InitializeComboBox()
        {     
            string errorMessage;
            var configUrls = FormsHelper.LoadUrlsFromSettings(out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                lblHttpStatus.Text = errorMessage;
                lblHttpStatus.ForeColor = Color.Red; 
                return;
            }
            if (configUrls.Any())
            {
                cmbUrls.Items.AddRange(configUrls.ToArray());
                cmbUrls.Text = configUrls[0]; 
            }
            else
            {
                cmbUrls.Text = "Enter or select a sitemap URL"; 
            }          
            cmbUrls.GotFocus += (s, e) =>
            {
                if (cmbUrls.Text == "Enter or select a sitemap URL")
                    cmbUrls.Text = "";
            };
            cmbUrls.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(cmbUrls.Text))
                    cmbUrls.Text = "Enter or select a sitemap URL";
            };

            cmbUrls.TextChanged += (s, e) =>
            {
                cmbUrls.BackColor = cmbUrls.Text.StartsWith("http")
                    ? Color.White
                    : Color.Pink;
            };
        }
        private void SetStatusLabel(string message, bool isError = false, bool isSuccess = false)
        {
            lblHttpStatus.Text = message;
            if (isError)
            {
                lblHttpStatus.ForeColor = Color.Red; 
            }
            else if (isSuccess)
            {
                lblHttpStatus.ForeColor = Color.Green; 
            }
            else
            {
                lblHttpStatus.ForeColor = Color.Black; 
            }
        }

        private async void btnGo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbUrls.Text) || !cmbUrls.Text.StartsWith("http"))
            {
                SetStatusLabel("Error: Enter a valid URL (http:// or https://)", isError: true);
                return;
            }
            btnGo.Enabled = false; 
            SetStatusLabel("Status: Loading...");

            try
            {               
                var xmlFilePath = cmbUrls.Text;
                var urls = await _service.DownloadAndParse(xmlFilePath);
                var csvFilePath = FormsHelper.GetOutputFilePath();
                if (csvFilePath != null)
                {
                    _service.SaveToCsv(urls, csvFilePath);
                    SetStatusLabel($"Status: Extracted {urls.Count} URLs, saved to {csvFilePath}", isSuccess: true);
                }
                else
                {
                    SetStatusLabel("Status: Save canceled");
                }
            }
            catch (Exception ex)
            {
                //SetStatusLabel($"Error: {ex.Message}", isError: true);
                SetStatusLabel($"Error: Couldn`t connect to resource", isError: true);
            }
            finally
            {
                btnGo.Enabled = true; 
            }
        }

    }
}
