using Microsoft.EntityFrameworkCore;
using TrananAPI.DTO;
using TrananAPI.Models;

namespace TrananAPI.Data;

public class ReservationRepository
{
    private readonly TrananDbContext _trananDbContext;

    public ReservationRepository(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<List<ReservationDTO>> GetReservations()
    {
        try
        {
            if (_trananDbContext.Movies.Count() < 1)
            {
                return new List<ReservationDTO>();
            }
            return await _trananDbContext.Reservations
                .Include(r => r.Seats)
                .Include(r => r.Customer)
                .Include(r => r.MovieScreening)
                .Select(r => Mapper.GenerateReservationDTO(r))
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<List<ReservationDTO>> GetReservationsByScreeningId(int screeningId)
    {
        try
        {
            var reservations = await _trananDbContext.Reservations
                .Include(r => r.Customer)
                .Include(r => r.MovieScreening)
                .Include(r => r.Seats)
                .Where(r => r.MovieScreeningId == screeningId)
                .ToListAsync();

            return reservations.Select(r => Mapper.GenerateReservationDTO(r)).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<ReservationDTO> CreateReservation(ReservationDTO reservationDTO)
    {
        try
        {
            var newReservation = await Mapper.GenerateReservation(reservationDTO);
            List<Seat> seats = new();
            foreach(var seat in newReservation.Seats)
            {
                var foundSet = await _trananDbContext.Seats.FindAsync(seat.SeatId);
                seats.Add(foundSet);
            }
            newReservation.Seats = seats;
            await _trananDbContext.Reservations.AddAsync(newReservation);
            await _trananDbContext.SaveChangesAsync();
            var recentlyAddedReservation = await _trananDbContext.Reservations
                .OrderByDescending(r => r.ReservationId)
                .FirstAsync();
            return Mapper.GenerateReservationDTO(recentlyAddedReservation);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    // public async Task UpdateMovie(MovieDTO movieDTO)
    // {
    //     try
    //     {
    //         var movie = await _trananDbContext.Movies
    //             .Include(m => m.Actors)
    //             .FirstAsync(m => m.MovieId == movieDTO.MovieId);
    //         if (movie == null)
    //         {
    //             throw new Exception("No movie found");
    //         }

    //         //får göra såhär annars skapar ny instans och då skapas ny film ist för upppdatera
    //         movie.Title = movieDTO.Title;
    //         movie.Language = movieDTO.Language;
    //         movie.ReleaseYear = movieDTO.ReleaseYear;
    //         movie.DurationSeconds = movieDTO.DurationSeconds;

    //         List<Actor> updatedActors = new();
    //         foreach (var actorDTO in movieDTO.ActorDTOs)
    //         {
    //             var existingActor = await _trananDbContext.Actors.FindAsync(actorDTO.ActorId);
    //             if (existingActor == null)
    //             {
    //                 _trananDbContext.Actors.Add(Mapper.GenerateActor(actorDTO));
    //                 var recentlyAddedActor = movie.Actors
    //                     .OrderByDescending(a => a.ActorId)
    //                     .FirstOrDefault();
    //                 updatedActors.Add(recentlyAddedActor);
    //             }
    //             else
    //             {
    //                 updatedActors.Add(existingActor);
    //             }
    //         }
    //         movie.Actors = updatedActors;
    //         _trananDbContext.Movies.Update(movie);
    //         await _trananDbContext.SaveChangesAsync();
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e.Message);
    //     }
    // }

    // public async Task<MovieDTO> AddMovie(MovieDTO movieDTO)
    // {
    //     await _trananDbContext.Movies.AddAsync(Mapper.GenerateMovie(movieDTO));
    //     await _trananDbContext.SaveChangesAsync();
    //     return movieDTO;
    // }

    // public async Task DeleteMovie(MovieDTO movieDTO)
    // {
    //     var movieToDelete = await _trananDbContext.Movies.FindAsync(movieDTO.MovieId);
    //     _trananDbContext.Movies.Remove(movieToDelete);
    //     await _trananDbContext.SaveChangesAsync();
    // }

    // public async Task DeleteMovies()
    // {
    //     _trananDbContext.Movies.ToList().ForEach(m => _trananDbContext.Movies.Remove(m));
    //     await _trananDbContext.SaveChangesAsync();
    // }

    // private List<Movie> GenerateRandomMovies()
    // {
    //     var actors = new List<Actor>()
    //     {
    //         new Actor("Isabella", "Tortellini"),
    //         new Actor("Henrik", "Byström")
    //     };
    //     List<Movie> movies =
    //         new()
    //         {
    //             new Movie("Harry Potter", 2023, "English", 208,"Let's go to hogwarts", actors),
    //             new Movie("Kalle Anka", 2023, "English", 208,"En anka som är tokig", actors),
    //             new Movie("Sagan om de sju", 2023, "English", 208,"Läskig men rolig", actors),
    //             new Movie("Milkshake", 2023, "English", 208,"My milkshake brings all..", actors),
    //             new Movie("Macarena", 2023, "English", 208,"Om macarena bandet med la ketchup", actors),
    //         };

    //     return movies;
    // }
}
