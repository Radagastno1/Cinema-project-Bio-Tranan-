using TrananMVC.Models;
using Newtonsoft.Json;

namespace TrananMVC.Repository;

public class MovieRepository
{
    const string url = "https://localhost:7070";
    public async Task<IEnumerable<Movie>> GetMovies()
    {
        Console.WriteLine("GET MOVIES ANROPAS");
        using HttpClient client = new();
        try
        {
            var jsonString = await client.GetStringAsync(url + "/movie");
            var movies = JsonConvert.DeserializeObject<List<Movie>>(jsonString);
            return movies;
        }
        catch (HttpRequestException)
        {
            return new List<Movie>();
        }
    }
}