using TrananMVC.Interface;
using Newtonsoft.Json;
using Core.Models;

namespace TrananMVC.ApiServices;

public class MovieTrailerService : IMovieTrailerService
{
    private string YoutubeApiKey { get; set; }
    private string TmdbApiKey { get; set; }

    public MovieTrailerService()
    {
        YoutubeApiKey = "AIzaSyC0ps8z67GJeu6G1EXaB1CMd8mpZBDINgg";
        TmdbApiKey = "6ad1feea19be83985f0e68f6816fb05b";
    }

    public async Task<string> GetYoutubeTrailerLinkByMovieId(Movie movie)
    {
        var trailerId = movie.TrailerId;
        using HttpClient client = new HttpClient();
        var response = await client.GetAsync(
            $"https://www.googleapis.com/youtube/v3/videos?id={trailerId}&key={YoutubeApiKey}"
        );
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var videoList = JsonConvert.DeserializeObject<TrananMVC.Model.YoutubeResponse>(json);

            if (videoList.Items.Any())
            {
                return $"https://www.youtube.com/watch?v={videoList.Items.First().Id}";
            }
        }
        return null;
    }

    public async Task<string> GetTMDBTrailerLinkByMovieId(Movie movie)
    {
        using HttpClient client = new HttpClient();
        var response = await client.GetAsync(
            $"https://api.themoviedb.org/3/movie/{movie.TrailerId}/videos?api_key={TmdbApiKey}&language=en-US"
        );
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var videoList = JsonConvert.DeserializeObject<TmdbResponse>(json);

            var trailer = videoList.Results.FirstOrDefault(v => v.Type == "Trailer");

            if (trailer != null)
            {
                return $"https://www.youtube.com/watch?v={trailer.Key}";
            }
        }
        return null;
    }
}
