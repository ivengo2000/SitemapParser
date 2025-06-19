using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace ParserLogic
{
    public class SitemapService
    {

        public async Task<List<string>> DownloadAndParse(string url)
        {
            //Console.WriteLine($"Downloading sitemap from: {url}");

            // Create a custom HttpClient with timeout and retry policy
            using (var client = new HttpClient(new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            }))
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; SitemapParser/1.0)");
                client.DefaultRequestHeaders.Accept.ParseAdd("application/xml, text/xml, */*");

                try
                {
                    // Use cancellation token for better timeout control
                    using (var cts = new CancellationTokenSource())
                    {
                        cts.CancelAfter(TimeSpan.FromSeconds(30));

                        // Use GetAsync with Stream for better memory efficiency
                        using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cts.Token).ConfigureAwait(false))
                        {
                            response.EnsureSuccessStatusCode();

                            // Read as stream to handle large sitemaps
                            using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                            using (var reader = new StreamReader(stream))
                            {
                                string xmlContent = await reader.ReadToEndAsync().ConfigureAwait(false);

                                return Parse(xmlContent);
                            }
                        }
                    }
                }
                catch (TaskCanceledException)
                {
                    throw new Exception("The request timed out.");
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception($"Failed to download sitemap: {ex.Message}");
                }
            }
        }

        public List<string> Parse(string xmlContent)
        {
            var urls = new List<string>();

            try
            {
                var doc = XDocument.Parse(xmlContent);
                XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

                // Parse regular URLs
                foreach (var urlElement in doc.Descendants(ns + "url"))
                {
                    var locElement = urlElement.Element(ns + "loc");
                    if (locElement != null)
                    {
                        urls.Add(locElement.Value.Trim());
                    }
                }

                // Parse sitemap index entries
                foreach (var sitemapElement in doc.Descendants(ns + "sitemap"))
                {
                    var locElement = sitemapElement.Element(ns + "loc");
                    if (locElement != null)
                    {
                        urls.Add(locElement.Value.Trim());
                    }
                }

                if (urls.Count == 0)
                {
                    throw new Exception("No URLs found in the sitemap. Check if the URL points to a valid sitemap.");
                }

                return urls;
            }
            catch (XmlException ex)
            {
                throw new Exception($"Invalid XML format: {ex.Message}");
            }
        }

        public void SaveToCsv(List<string> urls, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                // Write header
                writer.WriteLine("URL");

                // Write each URL
                foreach (var url in urls)
                {
                    // Escape commas if they exist in the URL
                    var escapedUrl = url.Contains(",") ? $"\"{url}\"" : url;
                    writer.WriteLine(escapedUrl);
                }
            }
        }
    }
}
