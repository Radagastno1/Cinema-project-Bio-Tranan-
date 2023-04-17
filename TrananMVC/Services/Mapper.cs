using Core.Models;
using TrananMVC.ViewModel;

namespace TrananMVC.Service;

public class Mapper
{
    public static Review GenerateReview(ReviewViewModel reviewViewModel)
    {
        var review = new Review()
        {
            Alias = reviewViewModel.Alias,
            Rating = reviewViewModel.Rating,
            Comment = reviewViewModel.Comment,
            ReservationCode = reviewViewModel.ReservationCode,
            MovieId = reviewViewModel.MovieViewModel.MovieId,
        };
        return review;
    }

    public static ReviewViewModel GenerateReviewAsViewModel(Review review)
    {
        var reviewViewModel = new ReviewViewModel(
            review.Rating,
            review.Comment,
            review.ReservationCode,
            Mapper.GenerateMovieAsViewModel(review.Movie)
        );
        return reviewViewModel;
    }

    public static MovieScreeningViewModel GenerateMovieScreeningToViewModel(
        MovieScreening movieScreening
    )
    {
        var movieScreeningViewModel = new MovieScreeningViewModel(
            movieScreening.MovieScreeningId,
            movieScreening.DateAndTime,
            movieScreening.Movie.MovieId,
            movieScreening.Movie.Title,
            movieScreening.Movie.ImageUrl,
            movieScreening.Theater.Name,
            movieScreening.PricePerPerson,
            GenerateAllSeats(movieScreening)
        );

        return movieScreeningViewModel ?? new MovieScreeningViewModel();
    }

    private static List<SeatViewModel> GenerateAllSeats(MovieScreening movieScreening)
    {
        var allSeats = new List<SeatViewModel>();
        foreach (var seat in movieScreening.Theater.Seats)
        {
            bool isBooked = false;
            if (movieScreening.ReservedSeats != null)
            {
                isBooked = movieScreening.ReservedSeats.Any(rs => rs.SeatId == seat.SeatId);
            }
            allSeats.Add(
                new SeatViewModel()
                {
                    SeatId = seat.SeatId,
                    Row = seat.Row,
                    SeatNumber = seat.SeatNumber,
                    IsBooked = isBooked,
                    IsNotBookable = seat.IsNotBookable,
                    IsWheelChairSpace = seat.IsWheelChairSpace
                }
            );
        }
        return allSeats;
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
            DirectorsAsViewModels(movie.Directors),
            movie.TrailerId
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
        );
        return reservationViewModel;
    }

    public static async Task<Reservation> GenerateReservation(
        ReservationViewModel reservationViewModel
    )
    {
        var reservation = await Reservation.CreateReservationAsync(
            reservationViewModel.ReservationId,
            reservationViewModel.Price,
            reservationViewModel.MovieScreeningId,
            GenerateCustomer(reservationViewModel.CustomerViewModel),
            GenerateSeatsFromIds(reservationViewModel.SeatIds),
            reservationViewModel.IsCheckedIn
        );
        return reservation;
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
        Reservation reservation,
        Movie movie
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
            movieScreening.DateAndTime,
            movie.Title,
            movie.ImageUrl,
            movieScreening.Theater.Name,
            GenerateSeatsToViewModels(reservation.Seats)
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
