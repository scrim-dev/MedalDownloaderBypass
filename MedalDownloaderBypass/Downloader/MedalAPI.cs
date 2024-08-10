using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedalDownloaderBypass.Downloader
{
    internal class MedalAPI
    {
        //API by https://github.com/Tyson3101
        //https://medalbypass.vercel.app/api/clip?url=

        public static readonly HttpClient _httpClient = new();
        public static async Task<string> GetJsonFromApiAsync(string url)
        {
            string requestUrl = $"https://medalbypass.vercel.app/api/clip?url={Uri.EscapeDataString(url)}";
            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}