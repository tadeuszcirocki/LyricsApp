using LyricsApp.Models.Interfaces;
using LyricsApp.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LyricsApp.Models
{
    public class ApiClient : IApiClient
    {
        public async Task<LyricsApiModel> GetLyrics(string artist, string title)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.lyrics.ovh");
                var response = await client.GetAsync($"v1/{artist}/{title}");
                response.EnsureSuccessStatusCode();

                string stringResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LyricsApiModel>(stringResult);

            }
        }
    }
}
