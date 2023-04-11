using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Collections.Generic;
using TrananMVC.Models;

namespace TrananMVC.Repository;

public class MovieScreeningRepository
{
    const string url = "https://localhost:7070";

    public async Task<IEnumerable<MovieScreening>> GetUpcomingMovieScreenings()
    {
        using HttpClient client = new();
        try
        {
            var jsonString = await client.GetStringAsync(url + "/moviescreening");
            var movieScreenings = JsonConvert.DeserializeObject<List<MovieScreening>>(jsonString);
            return movieScreenings;
        }
        catch (HttpRequestException)
        {
            return new List<MovieScreening>();
        }
    }
}
