using LyricsApp.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsApp.Models.ViewModels
{
    public class DisplayLyricsPageModel
    {
        public string lyrics { get; set; }
        public FavouriteSong favouriteSong { get; set; }
    }
}
