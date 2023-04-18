using Core.Models;
using Microsoft.EntityFrameworkCore;
using Core.Interface;

namespace Core.Data.Repository;

public class TheaterRepository : IRepository<Theater>
{
    private readonly TrananDbContext _trananDbContext;

    public TheaterRepository(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<IEnumerable<Theater>> GetAsync()
    {
        try
        {
            if (_trananDbContext.Theaters.Count() < 1)
            {
                return new List<Theater>();
            }
            var theaters = await _trananDbContext.Theaters.Include(t => t.Seats).ToListAsync();
            if (theaters == null)
            {
                return new List<Theater>();
            }
            return theaters;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<Theater> GetByIdAsync(int id)
    {
        try
        {
            var theater = await _trananDbContext.Theaters
                .Include(t => t.Seats)
                .FirstAsync(t => t.TheaterId == id);
            if (theater == null)
            {
                return null;
            }
            return theater;
        }
        catch (Exception e)
        {
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
            theaterToUpdate.TheaterPrice = theater.TheaterPrice;

            _trananDbContext.Theaters.Update(theaterToUpdate);
            await _trananDbContext.SaveChangesAsync();
            return theaterToUpdate;
        }
        catch (Exception e)
        {
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
