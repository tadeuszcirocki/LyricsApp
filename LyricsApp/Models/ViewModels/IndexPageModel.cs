using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LyricsApp.Models;
using LyricsApp.Models.Database;

namespace LyricsApp.Models.ViewModels
{
    public class IndexPageModel
    {
        public Data data { get; set; }
        public IEnumerable <FavouriteSong> favouriteSong { get; set; }
    }
}
