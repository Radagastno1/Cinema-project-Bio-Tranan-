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
            MovieId = movieScreening.Movie.MovieId,
            MovieTitle = movieScreening.Movie.Title,
            MovieImageUrl = movieScreening.Movie.ImageUrl,
            TheaterName = movieScreening.TheaterName,
            AvailebleSeats = GenerateSeatsToViewModels(
                movieScreening.AllSeats
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
        return new ReservationViewModel()
        {
            ReservationId = reservation.ReservationId,
            ReservationCode = reservation.ReservationCode,
            Price = reservation.Price,
            MovieScreeningId = reservation.MovieScreeningId,
            CustomerViewModel = new CustomerViewModel()
            {
                CustomerId = reservation.Customer.CustomerId,
                FirstName = reservation.Customer.FirstName,
                LastName = reservation.Customer.LastName,
                PhoneNumber = reservation.Customer.PhoneNumber,
                Email = reservation.Customer.Email
            },
            SeatIds = reservation.SeatIds
        };
    }

    public static Reservation GenerateReservation(ReservationViewModel reservationViewModel)
    {
        return new Reservation()
        {
            ReservationId = reservationViewModel.ReservationId,
            ReservationCode = reservationViewModel.ReservationCode,
            Price = reservationViewModel.Price,
            MovieScreeningId = reservationViewModel.MovieScreeningId,
            Customer = new Customer()
            {
                CustomerId = reservationViewModel.CustomerViewModel.CustomerId,
                FirstName = reservationViewModel.CustomerViewModel.FirstName,
                LastName = reservationViewModel.CustomerViewModel.LastName,
                PhoneNumber = reservationViewModel.CustomerViewModel.PhoneNumber,
                Email = reservationViewModel.CustomerViewModel.Email
            },
            SeatIds = reservationViewModel.SeatIds
        };
    }

    public static CreateReservationViewModel GenerateCreateReservationViewModel(
        Reservation reservation,
        MovieScreening movieScreening
    )
    {
        return new CreateReservationViewModel()
        {
            ReservationViewModel = GenerateReservationViewModel(reservation),
            MovieScreeningViewModel = GenerateMovieScreeningToViewModel(movieScreening)
        };
    }

    //   public static ReservationViewModel GenerateReservationViewModel(CreateReservationViewModel createReservationViewModel)
    // {
    //     return new ReservationViewModel()
    //     {
    //         ReservationId = createReservationViewModel.ReservationId,
    //         ReservationCode = createReservationViewModel.ReservationCode,
    //         MovieScreeningId = createReservationViewModel.MovieScreeningId,
    //         SeatIds = createReservationViewModel.SeatIds,
    //         CustomerViewModel = GenerateCustomerViewModel(createReservationViewModel.Customer)
    //     };
    // }

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
        return new Customer(
            customerViewModel.CustomerId,
            customerViewModel.FirstName,
            customerViewModel.LastName,
            customerViewModel.PhoneNumber,
            customerViewModel.Email
        );
    }

    public static CustomerViewModel GenerateCustomerViewModel(Customer customer)
    {
        return new CustomerViewModel()
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email
        };
    }
}
