using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody_Windows.Search
{
    public enum SearchType
    {
        Tracks, Videos, Albums, SpotifyPlaylists, YouTubePlaylists

    }
    public static class SearchTypeExtensions
    {
        public static Dictionary<string, SearchType> SearchTypeDictionary
            = new Dictionary<string, SearchType>()
            {
                {"tracks",SearchType.Tracks},
                {"videos", SearchType.Videos},
                {"albums",SearchType.Albums},
                {"spotifyplaylists", SearchType.SpotifyPlaylists},
                {"youtubeplaylists", SearchType.YouTubePlaylists }
            };
        public static SearchType ParseSearchType(this string str)
        {
            return SearchTypeDictionary[str.Trim().ToLower()];
        }
    }
}
