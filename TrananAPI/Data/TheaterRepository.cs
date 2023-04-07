using TrananAPI.DTO;
using TrananAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace TrananAPI.Data;

public class TheaterRepository
{
    //har hand om salong och dess stolar 
    private readonly TrananDbContext _trananDbContext;

    public TheaterRepository(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<List<TheaterDTO>> GetTheaters()
    {
        try
        {
            if (_trananDbContext.Theaters.Count() < 1)
            {
                var randomTheater = GenerateRandomTheater();
                await _trananDbContext.Theaters.AddAsync(randomTheater);
                await _trananDbContext.Seats.AddRangeAsync(randomTheater.Seats);
                await _trananDbContext.SaveChangesAsync();
            }
            var theaters = await _trananDbContext.Theaters.Include(t => t.Seats).ToListAsync();
            return theaters.Select(t => Mapper.GenerateTheaterDTO(t)).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public async Task<TheaterDTO> GetTheaterById(int id)
    {
        try
        {
            var theater =  await _trananDbContext.Theaters.Include(t => t.Seats).FirstAsync(t => t.TheaterId == id); 
            return Mapper.GenerateTheaterDTO(theater);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<TheaterDTO> CreateTheater(TheaterDTO theaterDTO)
    {
        try
        {
            await _trananDbContext.Theaters.AddAsync(Mapper.GenerateTheater(theaterDTO));
            await _trananDbContext.Seats.AddRangeAsync(Mapper.GenerateSeats(theaterDTO.SeatDTOs));
            await _trananDbContext.SaveChangesAsync();
            return theaterDTO;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task UpdateTheater(TheaterDTO theaterDTO)
    {
        try
        {

            _trananDbContext.Theaters.Update(Mapper.GenerateTheater(theaterDTO));
            _trananDbContext.Seats.UpdateRange(Mapper.GenerateSeats(theaterDTO.SeatDTOs));
            await _trananDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task AddTheater(TheaterDTO theaterDTO)
    {
        await _trananDbContext.Theaters.AddAsync(Mapper.GenerateTheater(theaterDTO));
        await _trananDbContext.SaveChangesAsync();
    }

    public async Task DeleteTheater(TheaterDTO theaterDTO)
    {
        _trananDbContext.Theaters.Remove(Mapper.GenerateTheater(theaterDTO));
        await _trananDbContext.SaveChangesAsync();
    }
    private Theater GenerateRandomTheater()
    {
        var seats = new List<Seat>()
        {
            new Seat(1, 1),
            new Seat(1, 2),
            new Seat(1, 3),
            new Seat(1, 4),
            new Seat(1, 5)
        };
        var theater = new Theater("Tranan123", 25, seats);
        return theater;
    }
}
