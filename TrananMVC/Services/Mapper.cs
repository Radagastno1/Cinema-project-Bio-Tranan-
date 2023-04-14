using Core.Models;
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
            MovieId = movieScreening.Movie.MovieId,
            MovieTitle = movieScreening.Movie.Title,
            MovieImageUrl = movieScreening.Movie.ImageUrl,
            TheaterName = movieScreening.Theater.Name,
            AvailebleSeats = GenerateSeatsToViewModels(
                movieScreening.Theater.Seats
                    .Where(s => s.IsBooked == false && s.IsNotBookable == false)
                    .ToList()
            ) //om man nu ska välja detta redan här? kolla på det
        };
        return movieScreeningViewModel ?? new MovieScreeningViewModel();
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

    public static List<SeatViewModel> GenerateSeatsToViewModels(List<Seat> seats)
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
        return new MovieViewModel(
            movie.MovieId,
            movie.Title,
            movie.ReleaseYear,
            movie.Language,
            movie.Description,
            movie.DurationMinutes,
            movie.MaxScreenings,
            movie.AmountOfScreenings,
            movie.ImageUrl,
            ActorsAsViewModels(movie.Actors),
            DirectorsAsViewModels(movie.Directors)
        );
    }

    public static ReservationViewModel GenerateReservationViewModel(Reservation reservation)
    {
        var customerViewModel = new CustomerViewModel(
            reservation.Customer.CustomerId,
            reservation.Customer.FirstName,
            reservation.Customer.LastName,
            reservation.Customer.PhoneNumber,
            reservation.Customer.Email
        );
        var reservationViewModel = new ReservationViewModel(
            reservation.ReservationId,
            reservation.Price,
            reservation.ReservationCode,
            reservation.MovieScreeningId,
            customerViewModel,
            reservation.Seats.Select(s => s.SeatId).ToList()
        ///ändrat här så kanske ej funkar
        );
        return reservationViewModel;
    }

    public static Reservation GenerateReservation(ReservationViewModel reservationViewModel)
    {
        return new Reservation()
        {
            ReservationId = reservationViewModel.ReservationId,
            ReservationCode = reservationViewModel.ReservationCode,
            Price = reservationViewModel.Price,
            MovieScreeningId = reservationViewModel.MovieScreeningId,
            Customer = new Customer(
                reservationViewModel.CustomerViewModel.FirstName,
                reservationViewModel.CustomerViewModel.LastName,
                reservationViewModel.CustomerViewModel.PhoneNumber,
                reservationViewModel.CustomerViewModel.Email
            )
            {
                CustomerId = reservationViewModel.CustomerViewModel.CustomerId,
            },
            Seats = GenerateSeatsFromIds(reservationViewModel.SeatIds)
        };
    }

    public static List<Seat> GenerateSeatsFromIds(List<int> ids)
    {
        List<Seat> seats = new();
        ids.ForEach(id => seats.Add(new Seat() { SeatId = id }));
        return seats;
    }

    public static MovieScreeningReservationViewModel GenerateMovieReservationViewModel(
        Reservation reservation,
        MovieScreening movieScreening
    )
    {
        return new MovieScreeningReservationViewModel()
        {
            ReservationViewModel = GenerateReservationViewModel(reservation),
            MovieScreeningViewModel = GenerateMovieScreeningToViewModel(movieScreening)
        };
    }

    public static CreatedReservationViewModel GenerateCreatedReservationViewModel(
        MovieScreening movieScreening,
        Reservation reservation, Movie movie
    )
    {
        return new CreatedReservationViewModel(
            reservation.ReservationId,
            reservation.ReservationCode,
            reservation.Price,
            reservation.Customer.FirstName,
            reservation.Customer.LastName,
            reservation.Customer.PhoneNumber,
            reservation.Customer.Email,
            reservation.Seats.Select(s => s.SeatId).ToList(), //kund vill ej ha idn! fixa
            movieScreening.DateAndTime,
            movie.Title,
            movie.ImageUrl,
            movieScreening.Theater.Name
        );
    }

    public static List<ActorViewModel> ActorsAsViewModels(List<Actor> actors)
    {
        List<ActorViewModel> actorViewModels = new();
        foreach (var actor in actors)
        {
            actorViewModels.Add(new ActorViewModel(actor.ActorId, actor.FirstName, actor.LastName));
        }
        return actorViewModels;
    }

    public static List<DirectorViewModel> DirectorsAsViewModels(List<Director> directors)
    {
        List<DirectorViewModel> directorViewModels = new();
        foreach (var director in directors)
        {
            directorViewModels.Add(
                new DirectorViewModel(director.DirectorId, director.FirstName, director.LastName)
            );
        }
        return directorViewModels;
    }

    public static Customer GenerateCustomer(CustomerViewModel customerViewModel)
    {
        var customer = new Customer(
            customerViewModel.FirstName,
            customerViewModel.LastName,
            customerViewModel.PhoneNumber,
            customerViewModel.Email
        );
        customer.CustomerId = customerViewModel.CustomerId;
        return customer;
    }
}
