using TrananMVC.Models;
namespace TrananMVC.Repository;

public class MovieRepository
{
    const string url = "https://localhost:7070";
    public async Task<IEnumerable<Movie>> GetMovies()
    {
        using HttpClient client = new();
        try
        {
            var movies = await client.GetFromJsonAsync<List<Movie>>(url + "/movie");
            return movies;
        }
        catch (HttpRequestException)
        {
            return new List<Movie>();
        }
    }
}