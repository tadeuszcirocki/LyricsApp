using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LyricsApp.Models.ViewModels;
using LyricsApp.Models.Interfaces;
using LyricsApp.Models.Database;

namespace LyricsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiClient _apiClient; //DI - one ApiClient for whole controller
        private readonly FavouriteSongContext _db;

        public HomeController(IApiClient apiClient, FavouriteSongContext db)
        {
            _apiClient = apiClient;
            _db = db;
        }

        public IActionResult Index()
        {
            var tuple = Tuple.Create<IEnumerable<FavouriteSong>,Data>(_db.Songs.ToList(), null);
            return View(tuple);
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

        public IActionResult GetLyricsByArtistAndTitle(Data Item2)
        {
            Task<LyricsApiModel> lyricsObj = _apiClient.GetLyrics(Item2.artist, Item2.title);
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
