using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LyricsApp.Models;
using LyricsApp.Models.Interfaces;

namespace LyricsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiClient _apiClient; //DI - jeden ApiClient na caly kontroler

        public HomeController(IApiClient apiClient) //
        {
            _apiClient = apiClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetLyricsByArtistAndTitle(Data _artist, Data _title)
        {
            Task<LyricsApiModel> lyricsObj = _apiClient.GetLyrics(_artist.artist, _title.title);
            return View(model: lyricsObj.Result); //przekazujemy model     
        }
    }
}
