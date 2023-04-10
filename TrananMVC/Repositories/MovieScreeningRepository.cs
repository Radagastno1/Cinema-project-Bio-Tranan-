using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Collections.Generic;
using TrananMVC.ViewModel;
namespace TrananMVC.Repository;

public class MovieScreeningRepository
{
    HttpClient client = new();
    const string url = "";

    public async Task<List<MovieScreeningViewModel>> GetUpcomingMovieScreenings()
    {
         try
        {
            var movieScreenings = await client.GetFromJsonAsync<List<MovieScreeningViewModel>>(url);
            return movieScreenings;
        }
        catch (HttpRequestException)
        {
            return new List<MovieScreeningViewModel>();
        }
    }
}