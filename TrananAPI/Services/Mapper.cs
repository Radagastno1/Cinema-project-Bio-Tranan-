using Core.Models;
using TrananAPI.DTO;

namespace TrananAPI.Service.Mapper;

public class Mapper
{
    public static MovieDTO GenerateMovieDTO(Movie movie)
    {
        var movieDTO = new MovieDTO(
            movie.MovieId,
            movie.Title,
            movie.ReleaseYear,
            movie.Language,
            movie.DurationMinutes,
            movie.Description,
            movie.AmountOfScreenings,
            movie.MaxScreenings,
            movie.ImageUrl,
            movie.Actors.Select(a => GenerateActorDTO(a)).ToList(),
            movie.Directors.Select(d => GenerateDirectorDTO(d)).ToList(),
            movie.Price,
            movie.TrailerId ?? null
        );
        return movieDTO;
    }

    public static Movie GenerateMovie(MovieDTO movieDTO)
    {
        var movie = new Movie(
            movieDTO.Id,
            movieDTO.Title,
            movieDTO.ReleaseYear,
            movieDTO.Language,
            movieDTO.DurationMinutes,
            movieDTO.Description,
            movieDTO.AmountOfScreenings,
            movieDTO.MaxScreenings,
            movieDTO.ImageUrl,
            movieDTO.ActorDTOs.Select(a => GenerateActor(a)).ToList(),
            movieDTO.DirectorDTOs.Select(d => GenerateDirector(d)).ToList(),
            movieDTO.Price,
            movieDTO.TrailerId ?? null
        );
        return movie;
    }

    public static ActorDTO GenerateActorDTO(Actor actor)
    {
        var actorDTO = new ActorDTO(actor.ActorId, actor.FirstName, actor.LastName);
        return actorDTO;
    }

    public static Actor GenerateActor(ActorDTO actorDTO)
    {
        var actor = new Actor(actorDTO.FirstName, actorDTO.LastName);
        return actor;
    }

    public static DirectorDTO GenerateDirectorDTO(Director director)
    {
        var directorDTO = new DirectorDTO(
            director.DirectorId,
            director.FirstName,
            director.LastName
        );
        return directorDTO;
    }

    public static Director GenerateDirector(DirectorDTO directorDTO)
    {
        var director = new Director(directorDTO.FirstName, directorDTO.LastName);
        return director;
    }

    public static MovieScreening GenerateMovieScreeningFromIncomingDTO(
        MovieScreeningIncomingDTO movieScreeningDTO,
        Movie movie,
        Theater theater
    )
    {
        var movieScreening = new MovieScreening(movieScreeningDTO.DateAndTime, movie, theater);
        return movieScreening;
    }

    public static MovieScreeningIncomingDTO GenerateMovieScreeningIncomingDTO(
        MovieScreening movieScreening,
        Movie movie,
        Theater theater
    )
    {
        var movieScreeningDTO = new MovieScreeningIncomingDTO(
            movieScreening.DateAndTime,
            movie.MovieId,
            theater.TheaterId
        );
        return movieScreeningDTO;
    }

    public static MovieScreeningOutgoingDTO GenerateMovieScreeningOutcomingDTO(
        MovieScreening movieScreening
    )
    {
        var movieScreeningDTO = new MovieScreeningOutgoingDTO(
            movieScreening.MovieScreeningId,
            movieScreening.DateAndTime,
            GenerateMovieDTO(movieScreening.Movie),
            movieScreening.Theater.Name,
            GenerateAllSeats(movieScreening),
            movieScreening.PricePerPerson
        );
        return movieScreeningDTO;
    }

    private static List<SeatDTO> GenerateAllSeats(MovieScreening movieScreening)
    {
        var allSeats = new List<SeatDTO>();
        foreach (var seat in movieScreening.Theater.Seats)
        {
            bool isBooked = false;
            if (movieScreening.ReservedSeats != null)
            {
                isBooked = movieScreening.ReservedSeats.Any(rs => rs.SeatId == seat.SeatId);
            }
            allSeats.Add(
                new SeatDTO()
                {
                    Id = seat.SeatId,
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

    public static Theater GenerateTheater(TheaterDTO theaterDTO)
    {
        var theater = new Theater(
            theaterDTO.Id,
            theaterDTO.Name,
            theaterDTO.Rows,
            GenerateSeats(theaterDTO.SeatDTOs),
            theaterDTO.TheaterPrice
        );
        return theater;
    }

    public static TheaterDTO GenerateTheaterDTO(Theater theater)
    {
        var theaterDTO = new TheaterDTO(
            theater.TheaterId,
            theater.Name,
            theater.Rows,
            GenerateSeatsDTO(theater.Seats),
            theater.TheaterPrice
        );
        return theaterDTO;
    }

    public static ReservationDTO GenerateReservationDTO(Reservation reservation)
    {
        var reservationDTO = new ReservationDTO(
            reservation.ReservationId,
            reservation.Price,
            reservation.MovieScreeningId,
            GenerateCustomerDTO(reservation.Customer),
            reservation.Seats.Select(s => s.SeatId).ToList(),
            reservation.ReservationCode,
            reservation.IsCheckedIn
        );
        return reservationDTO;
    }

    public static Reservation GenerateReservation(ReservationDTO reservationDTO)
    {
        var reservation = new Reservation()
        {
            ReservationId = reservationDTO.Id,
            Price = reservationDTO.Price,
            MovieScreeningId = reservationDTO.MovieScreeningId,
            Customer = GenerateCustomer(reservationDTO.CustomerDTO),
            Seats = GenerateSeatsFromIds(reservationDTO.SeatIds),
            ReservationCode = reservationDTO.ReservationCode,
            IsCheckedIn = reservationDTO.IsCheckedIn
        };
        return reservation;
    }

    public static List<Seat> GenerateSeatsFromIds(List<int> ids)
    {
        List<Seat> seats = new();
        ids.ForEach(id => seats.Add(new Seat() { SeatId = id }));
        return seats;
    }

    public static CustomerDTO GenerateCustomerDTO(Customer customer)
    {
        return new CustomerDTO(
            customer.CustomerId,
            customer.FirstName,
            customer.LastName,
            customer.PhoneNumber,
            customer.Email
        );
    }

    public static Customer GenerateCustomer(CustomerDTO customerDTO)
    {
        return new Customer(
            customerDTO.FirstName,
            customerDTO.LastName,
            customerDTO.PhoneNumber,
            customerDTO.Email
        );
    }

    public static List<Seat> GenerateSeats(List<SeatDTO> seatDTOs)
    {
        return seatDTOs.Select(s => new Seat(s.SeatNumber, s.Row)).ToList();
    }

    public static List<SeatDTO> GenerateSeatsDTO(List<Seat> seats)
    {
        return seats
            .Select(
                s =>
                    new SeatDTO(
                        s.SeatId,
                        s.SeatNumber,
                        s.Row,
                        s.IsBooked,
                        s.IsNotBookable,
                        s.IsWheelChairSpace
                    )
            )
            .ToList();
    }
}
