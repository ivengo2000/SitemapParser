using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using SitemapParser.WInForms.UI;

namespace SitemapParser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Bypass SSL certificate validation (for testing only)
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(BypassSslValidation);

            Application.Run(new MainForm());
        }

        private static bool BypassSslValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Accept all certificates (for testing only!)
            return true;
        }
    }
}
