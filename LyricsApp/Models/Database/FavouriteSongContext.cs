using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsApp.Models.Database
{
    public class FavouriteSongContext : DbContext
    {
        public FavouriteSongContext(DbContextOptions<FavouriteSongContext> options) : base(options)
        {

        }

        public DbSet<FavouriteSong> Songs { get; set; }
    }
}
