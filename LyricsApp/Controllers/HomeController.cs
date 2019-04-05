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
        private readonly IApiClient _apiClient; //DI - one ApiClient for whole controller

        public HomeController(IApiClient apiClient) //
        {
            _apiClient = apiClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexWithError()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetLyricsByArtistAndTitle(Data song)
        {
            Task<LyricsApiModel> lyricsObj = _apiClient.GetLyrics(song.artist, song.title);
            try
            {
                return View(lyricsObj.Result);      //passing model to view
            }
            catch(AggregateException)
            {
                return RedirectToAction(nameof(IndexWithError));
            }
        }
    }
}
