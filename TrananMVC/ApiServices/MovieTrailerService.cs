using TrananMVC.Interface;
using Newtonsoft.Json;
using Core.Models;

namespace TrananMVC.ApiServices;

public class MovieTrailerService : IMovieTrailerService
{
    private string ApiKey { get; set; }
    public MovieTrailerService()
    {
        ApiKey = "AIzaSyC0ps8z67GJeu6G1EXaB1CMd8mpZBDINgg";
    }

    public async Task<string> GetTrailerLinkByMovieId(Movie movie)
    {
        var trailerId = movie.TrailerId;
        using HttpClient client = new HttpClient();
        var response = await client.GetAsync(
            $"https://www.googleapis.com/youtube/v3/videos?id={trailerId}&key={ApiKey}"
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
}
