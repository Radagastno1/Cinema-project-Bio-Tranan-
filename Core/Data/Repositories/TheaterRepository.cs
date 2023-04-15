using Core.Models;
using Microsoft.EntityFrameworkCore;
using Core.Interface;

namespace Core.Data.Repository;

public class TheaterRepository : IRepository<Theater>
{
    //har hand om salong och dess stolar
    private readonly TrananDbContext _trananDbContext;

    public TheaterRepository(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<List<Theater>> GetAsync()
    {
        try
        {
            if (_trananDbContext.Theaters.Count() < 1)
            {
                return new List<Theater>();
            }
            var theaters = await _trananDbContext.Theaters.Include(t => t.Seats).ToListAsync();
            return theaters;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public async Task<Theater> GetByIdAsync(int id)
    {
        try
        {
            var theater = await _trananDbContext.Theaters
                .Include(t => t.Seats)
                .FirstAsync(t => t.TheaterId == id);
            return theater;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<Theater> CreateAsync(Theater theater)
    {
        try
        {
            await _trananDbContext.Theaters.AddAsync(theater);
            await _trananDbContext.SaveChangesAsync();
            var recentlyAddedTheater = await _trananDbContext.Theaters
                .OrderByDescending(t => t.TheaterId)
                .FirstOrDefaultAsync();
            return recentlyAddedTheater;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<Theater> UpdateAsync(Theater theater)
    {
        try
        {
            var theaterToUpdate = await _trananDbContext.Theaters.FindAsync(theater.TheaterId);
            theaterToUpdate.Name = theater.Name ?? theaterToUpdate.Name;
            theaterToUpdate.Rows = theater.Rows;
            theaterToUpdate.Seats = theater.Seats;
            theaterToUpdate.MaxAmountAvailebleSeats = theater.MaxAmountAvailebleSeats;

            _trananDbContext.Theaters.Update(theaterToUpdate);
            await _trananDbContext.SaveChangesAsync();
            return theaterToUpdate;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }

    }

    public async Task DeleteByIdAsync(int id)
    {
        var theaterToDelete = await _trananDbContext.Theaters.FindAsync(id);
        _trananDbContext.Theaters.Remove(theaterToDelete);
        await _trananDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync()
    {
        throw new NotImplementedException();
    }
}
