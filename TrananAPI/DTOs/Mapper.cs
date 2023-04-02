using TrananAPI.Models;

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
            movie.Actors.Select(a => GenerateActorDTO(a)).ToList()
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
            movieDTO.ActorDTOs.Select(a => GenerateActor(a)).ToList() //to actors
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

    public static MovieScreening GenerateMovieScreeningFromIncomingDTO(
        MovieScreeningIncomingDTO movieScreeningDTO,
        Movie movie,
        Theater theater
    )
    {
        var movieScreening = new MovieScreening(
            movieScreeningDTO.DateAndTime,
            movie,
            theater
        );
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

    public static List<Seat> GenerateSeats(List<SeatDTO> seatDTOs)
    {
        return seatDTOs.Select(s => new Seat(s.SeatNumber, s.Row)).ToList();
    }

    public static List<SeatDTO> GenerateSeatsDTO(List<Seat> seats)
    {
        return seats.Select(s => new SeatDTO(s.SeatNumber, s.Row)).ToList();
    }
}
