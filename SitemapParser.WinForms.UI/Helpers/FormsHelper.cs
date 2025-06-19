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
    }
}
