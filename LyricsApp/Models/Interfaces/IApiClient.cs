using LyricsApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsApp.Models.Interfaces
{
    public interface IApiClient
    {
        Task<DisplayLyricsPageModel> GetLyrics(string artist, string title);
    }
}
