// using System.Net.Http;
// using System.Threading.Tasks;
// using Newtonsoft.Json;
// using System.Net.Http.Json;
// using System.Text;
// using System.Collections.Generic;
// using TrananMVC.Models;

// namespace TrananMVC.Repository;

// public class MovieScreeningRepository
// {
//     const string url = "https://localhost:7070/moviescreening";

//     public async Task<IEnumerable<MovieScreening>> GetUpcomingMovieScreenings()
//     {
//         using HttpClient client = new();
//         try
//         {
//             var jsonString = await client.GetStringAsync(url);
//             var movieScreenings = JsonConvert.DeserializeObject<List<MovieScreening>>(jsonString);
//             return movieScreenings;
//         }
//         catch (HttpRequestException)
//         {
//             return new List<MovieScreening>();
//         }
//     }

//     public async Task<MovieScreening> GetMovieScreeningById(int movieScreeningId)
//     {
//         using HttpClient client = new();
//         try
//         {
//             var jsonString = await client.GetStringAsync(url + $"/{movieScreeningId}");
//             var movieScreening = JsonConvert.DeserializeObject<MovieScreening>(jsonString);
//             return movieScreening;
//         }
//         catch (HttpRequestException)
//         {
//             return new MovieScreening();
//         }
//     }
// }
