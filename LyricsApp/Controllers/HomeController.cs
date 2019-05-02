using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LyricsApp.Models.ViewModels;
using LyricsApp.Models.Interfaces;
using LyricsApp.Models.Database;
using Microsoft.EntityFrameworkCore;

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
            var pageData = new IndexPageModel(_db.Songs.ToList());
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
            TempData["currentArtist"] = obj.data.artist; //using this to pass info about current song to AddToFavourites action
            TempData["currentTitle"] = obj.data.title;

            Task<DisplayLyricsPageModel> lyricsObj = _apiClient.GetLyrics(obj.data.artist, obj.data.title);
            try
            {
                return View(lyricsObj.Result);      //passing model to view
            }
            catch(AggregateException)
            {
                return RedirectToAction(nameof(IndexWithError));
            }
        }

        public async Task<IActionResult> AddToFavourites(FavouriteSong song)
        {
            song.Artist = TempData["currentArtist"].ToString();
            song.Title = TempData["currentTitle"].ToString();
            _db.Add(song);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveFromFavourites(FavouriteSong song)
        {
            var dbSong = await _db.Songs.SingleOrDefaultAsync(m => m.ID == song.ID);
            _db.Remove(dbSong);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
