using TrananMVC.Models;
using Newtonsoft.Json;

namespace TrananMVC.Repository;

public class MovieRepository
{
    const string url = "https://localhost:7070/movie";
    public async Task<IEnumerable<Movie>> GetMovies()
    {
        using HttpClient client = new();
        try
        {
            var jsonString = await client.GetStringAsync(url);
            var movies = JsonConvert.DeserializeObject<List<Movie>>(jsonString);
            return movies;
        }
        catch (HttpRequestException)
        {
            return new List<Movie>();
        }
    }
      public async Task<Movie> GetMovieById(int movieId)
    {
        using HttpClient client = new();
        try
        {
            var jsonString = await client.GetStringAsync(url + $"/{movieId}");
            var movie = JsonConvert.DeserializeObject<Movie>(jsonString);
            return movie;
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }
}