using TrananAPI.Models;
using TrananAPI.Service;

namespace TrananAPI.DTO;

public class Mapper
{
    public static MovieDTO GenerateMovieDTO(Movie movie)
    {
        var movieDTO = new MovieDTO(
            movie.MovieId,
            movie.Title,
            movie.ReleaseYear,
            movie.Language,
            movie.DurationSeconds,
            movie.Description,
            movie.Actors.Select(a => GenerateActorDTO(a)).ToList(), 
            movie.Directors.Select(d => GenerateDirectorDTO(d)).ToList()
        );
        return movieDTO;
    }

    public static Movie GenerateMovie(MovieDTO movieDTO)
    {
        var movie = new Movie(
            movieDTO.Title,
            movieDTO.ReleaseYear,
            movieDTO.Language,
            movieDTO.DurationSeconds,
            movieDTO.Description,
            movieDTO.ActorDTOs.Select(a => GenerateActor(a)).ToList(),
            movieDTO.DirectorDTOs.Select(d => GenerateDirector(d)).ToList()
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
        var directorDTO = new DirectorDTO(director.DirectorId, director.FirstName, director.LastName);
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
            movieScreening.DateAndTime,
            GenerateMovieDTO(movieScreening.Movie),
            GenerateTheaterDTO(movieScreening.Theater)
        );
        return movieScreeningDTO;
    }

    public static Theater GenerateTheater(TheaterDTO theaterDTO)
    {
        var theater = new Theater(
            theaterDTO.Name,
            theaterDTO.Rows,
            GenerateSeats(theaterDTO.SeatDTOs)
        );
        return theater;
    }

    public static TheaterDTO GenerateTheaterDTO(Theater theater)
    {
        var theaterDTO = new TheaterDTO(
            theater.Name,
            theater.Rows,
            GenerateSeatsDTO(theater.Seats)
        );
        return theaterDTO;
    }

    public static ReservationDTO GenerateReservationDTO(Reservation reservation)
    {
        var reservationDTO = new ReservationDTO(
            reservation.Price,
            reservation.MovieScreeningId,
            GenerateCustomerDTO(reservation.Customer),
            reservation.Seats.Select(s => s.SeatId).ToList(),
            reservation.ReservationCode
        );
        return reservationDTO;
    }

    public static async Task<Reservation> GenerateReservation(ReservationDTO reservationDTO)
    {
        var reservation = await Reservation.CreateReservationAsync(
            reservationDTO.Price,
            reservationDTO.MovieScreeningId,
            GenerateCustomer(reservationDTO.CustomerDTO),
            // await SeatService.GenerateSeatsFromIdsAsync(reservationDTO.SeatIds) //blir detta rätt async?
            GenerateSeatsFromIds(reservationDTO.SeatIds)
        );
        return reservation; //kollla upp om rätt
    }
    public static List<Seat> GenerateSeatsFromIds(List<int>ids)
    {
        List<Seat>seats = new();
        ids.ForEach(id => seats.Add(new Seat(){SeatId = id}));
        return seats;
    }
    public static CustomerDTO GenerateCustomerDTO(Customer customer)
    {
        return new CustomerDTO();
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
        return seats.Select(s => new SeatDTO(s.SeatNumber, s.Row)).ToList();
    }
}
