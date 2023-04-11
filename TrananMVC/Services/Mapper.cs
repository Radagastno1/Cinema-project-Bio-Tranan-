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
            Movie = GenerateMovieAsViewModel(movieScreening.Movie),
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
                    SeatNumber = seat.SeatNumber,
                    IsWheelChairSpace = seat.IsWheelChairSpace,
                    IsBooked = seat.IsBooked,
                    IsNotBookable = seat.IsNotBookable
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
                DurationMinutes = movie.DurationMinutes,
                MaxScreenings = movie.MaxScreenings,
                AmountOfScreenings = movie.AmountOfScreenings,
                ImageUrl = movie.ImageUrl,
                Actors = ActorsAsViewModels(movie.Actors),
                Directors = DirectorsAsViewModels(movie.Directors)
            };
    }
    public static List<ActorViewModel> ActorsAsViewModels(List<Actor>actors)
    {
        List<ActorViewModel>actorViewModels = new();
        foreach(var actor in actors)
        {
            actorViewModels.Add(new ActorViewModel(actor.ActorId, actor.FirstName, actor.LastName));
        }
        return actorViewModels;
    }
       public static List<DirectorViewModel> DirectorsAsViewModels(List<Director>directors)
    {
        List<DirectorViewModel>directorViewModels = new();
        foreach(var director in directors)
        {
            directorViewModels.Add(new DirectorViewModel(director.DirectorId, director.FirstName, director.LastName));
        }
        return directorViewModels;
    }
}
