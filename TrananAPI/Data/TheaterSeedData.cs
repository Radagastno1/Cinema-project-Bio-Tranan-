using TrananAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace TrananAPI.Data;

public class TheaterSeedData
{
    private readonly TrananDbContext _trananDbContext;

    public TheaterSeedData(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<List<Theater>> GetTheaters()
    {
        try
        {
            if (_trananDbContext.Theaters.Count() < 1)
            {
                await _trananDbContext.Theaters.AddAsync(GenerateRandomTheater());
                await _trananDbContext.SaveChangesAsync();
            }
            return await _trananDbContext.Theaters.Include(t => t.Seats).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public async Task<Theater> GetTheaterById(int id)
    {
        try
        {
            return await _trananDbContext.Theaters.Include(t => t.Seats).FirstAsync(t => t.TheaterId == id); //INCLUDE SEATS?
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<Theater> CreateTheater(Theater theater)
    {
        try
        {
            await _trananDbContext.Theaters.AddAsync(theater);
            await _trananDbContext.Seats.AddRangeAsync(theater.Seats);
            await _trananDbContext.SaveChangesAsync();
            return theater;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task UpdateTheater(Theater theater)
    {
        try
        {

            _trananDbContext.Theaters.Update(theater);
            _trananDbContext.Seats.UpdateRange(theater.Seats);
            await _trananDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task AddTheater(Theater theater)
    {
        await _trananDbContext.Theaters.AddAsync(theater);
        await _trananDbContext.SaveChangesAsync();
    }

    public async Task DeleteTheater(Theater theater)
    {
        _trananDbContext.Theaters.Remove(theater);
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
        var theater = new Theater("Tranan123", seats);
        return theater;
    }
}
