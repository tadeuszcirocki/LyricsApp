using LyricsApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsApp.Models.Interfaces
{
    public interface IApiClient
    {
        Task<LyricsApiModel> GetLyrics(string artist, string title);
    }
}
