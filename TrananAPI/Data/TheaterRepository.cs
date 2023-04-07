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
                return new List<TheaterDTO>();
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
            var theater = await _trananDbContext.Theaters
                .Include(t => t.Seats)
                .FirstAsync(t => t.TheaterId == id);
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
            Theater theater = Mapper.GenerateTheater(theaterDTO);
            await _trananDbContext.Theaters.AddAsync(theater);
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
}
