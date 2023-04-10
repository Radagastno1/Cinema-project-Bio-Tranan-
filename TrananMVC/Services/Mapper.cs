using TrananMVC.Models;
using TrananMVC.ViewModel;

namespace TrananMVC.Service;

public class Mapper
{
    public static MovieScreeningViewModel GenerateMovieScreeningToViewModel(
        MovieScreening movieScreening
    )
    {
        var movieScreeningViewModel = new MovieScreeningViewModel()
        {
            MovieScreeningId = movieScreening.MovieScreeningId,
            DateAndTime = movieScreening.DateAndTime,
            Movie = new MovieViewModel()
            {
                MovieId = movieScreening.Movie.MovieId,
                Title = movieScreening.Movie.Title,
                ReleaseYear = movieScreening.Movie.ReleaseYear,
                Language = movieScreening.Movie.Language,
                Description = movieScreening.Movie.Description,
            },
            MovieId = movieScreening.MovieId,
            TheaterId = movieScreening.TheaterId,
            Theater = GenerateTheaterToViewModel(movieScreening.Theater)
        };
        return movieScreeningViewModel;
    }

    public static TheaterViewModel GenerateTheaterToViewModel(Theater theater)
    {
        var theaterViewModel = new TheaterViewModel()
        {
            TheaterId = theater.TheaterId,
            Name = theater.Name,
            Rows = theater.Rows,
            Seats = GenerateSeatsToViewModels(theater.Seats)
        };
        return theaterViewModel;
    }

    private static List<SeatViewModel> GenerateSeatsToViewModels(List<Seat> seats)
    {
        List<SeatViewModel> seatViewModels = new();
        foreach (var seat in seats)
        {
            seatViewModels.Add(
                new SeatViewModel()
                {
                    SeatId = seat.SeatId,
                    Row = seat.Row,
                    SeatNumber = seat.SeatNumber
                }
            );
        }
        return seatViewModels;
    }

    public static MovieViewModel GenerateMovieAsViewModel(Movie movie)
    {
         return new MovieViewModel()
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                Language = movie.Language,
                Description = movie.Description,
            };
    }
}
