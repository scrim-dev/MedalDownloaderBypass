using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedalDownloaderBypass.Downloader
{
    internal class SaveFile
    {
        public static async Task SaveClipAsync(string url)
        {
            try
            {
                string jsonResponse = await MedalAPI.GetJsonFromApiAsync(url);
                var response = JObject.Parse(jsonResponse);

                if (response["valid"].Value<bool>())
                {
                    MessageBox.Show("File is valid! Press 'OK' to continue!", "Ready!", MessageBoxButton.OK, MessageBoxImage.Information);
                    string fileUrl = response["src"].Value<string>();
                    await DownloadFileAsync(fileUrl);
                }
                else
                {
                    MessageBox.Show($"Url error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static async Task DownloadFileAsync(string url)
        {
            using HttpResponseMessage response = await MedalAPI._httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            using FileStream fileStream = new($"Clip_{RandomInt()}.mp4", FileMode.Create, FileAccess.Write, FileShare.None);
            await response.Content.CopyToAsync(fileStream);
        }

        private static string RandomInt()
        {
            var random = new Random();
            int i = random.Next(9999, 99999);
            return i.ToString();
        }
    }
}