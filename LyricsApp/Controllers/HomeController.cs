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
            var pageData = new IndexPageModel(null,_db.Songs.ToList());
            return View(pageData);
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

        public IActionResult GetLyricsByArtistAndTitle(IndexPageModel obj)
        {
            Task<LyricsApiModel> lyricsObj = _apiClient.GetLyrics(obj.data.artist, obj.data.title);
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
