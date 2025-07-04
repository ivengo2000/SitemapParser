using SitemapParser.WInForms.UI.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SitemapParser.WInForms.UI.Helpers
{
    public static class FormsHelper
    {
        public static string GetOutputFilePath()
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = @"Save URLs as CSV";
                saveFileDialog.Filter = @"CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.DefaultExt = "csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return saveFileDialog.FileName;
                }
            }

            return null;
        }

        public static List<string> LoadUrlsFromSettings(out string errorMessage)
        {
            var urls = new List<string>();
            try
            {
                var settingsUrls = Settings.Default.Websites;
                if (settingsUrls != null)
                {
                    foreach (string url in settingsUrls)
                    {
                        if (!string.IsNullOrWhiteSpace(url) && url.StartsWith("http"))
                        {
                            urls.Add(url);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Error loading settings: {ex.Message}";
            }

            errorMessage = string.Empty;

            return urls;
        }


    }
}
