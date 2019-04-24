using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsApp.Models.Database
{
    public class FavouriteSong
    {   
        [Key]
        public int ID { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
    }
}
