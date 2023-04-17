using Core.Interface;
using Core.Models;

namespace Core.Services;

public class TheaterService : IService<Theater>
{
    private IRepository<Theater> _theaterRepository;

    public TheaterService(IRepository<Theater> theaterRepository)
    {
        _theaterRepository = theaterRepository;
    }

    public async Task<IEnumerable<Theater>> Get()
    {
        try
        {
            var theaters = await _theaterRepository.GetAsync();
            if (theaters == null)
            {
                return Enumerable.Empty<Theater>();
            }
            return theaters;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Theater> GetById(int theaterId)
    {
        try
        {
            var theater = await _theaterRepository.GetByIdAsync(theaterId);
            if (theater == null)
            {
                return new Theater();
            }
            return theater;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Theater> Create(Theater theater)
    {
        try
        {
            var addedTheater = await _theaterRepository.CreateAsync(theater);
            return addedTheater;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Theater> Update(Theater theater)
    {
        try
        {
            var updatedTheater = await _theaterRepository.UpdateAsync(theater);
            return updatedTheater;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task DeleteById(int theaterId)
    {
        await _theaterRepository.DeleteByIdAsync(theaterId);
    }
}
