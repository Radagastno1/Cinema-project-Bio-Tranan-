// using TrananMVC.Models;
// using Newtonsoft.Json;
// using System.Text;

// namespace TrananMVC.Repository;

// public class ReservationRepository
// {
//     const string url = "https://localhost:7070/reservation";

//     public async Task<Reservation> PostReservation(Reservation reservation)
//     {
//         using HttpClient client = new();
//         try
//         {
//             var json = JsonConvert.SerializeObject(reservation);
//             var content = new StringContent(json, Encoding.UTF8, "application/json");

//             var response = await client.PostAsync(url, content);
//             var responseString = await response.Content.ReadAsStringAsync();

//             var result = JsonConvert.DeserializeObject<Reservation>(responseString);
//             return result;
//         }
//         catch (HttpRequestException)
//         {
//             return new Reservation();
//         }
//     }
//     //   public async Task<Movie> GetMovieById(int movieId)
//     // {
//     //     using HttpClient client = new();
//     //     try
//     //     {
//     //         var jsonString = await client.GetStringAsync(url + $"/{movieId}");
//     //         var movie = JsonConvert.DeserializeObject<Movie>(jsonString);
//     //         return movie;
//     //     }
//     //     catch (HttpRequestException)
//     //     {
//     //         return null;
//     //     }
//     // }
// }
