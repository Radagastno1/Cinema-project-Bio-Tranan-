using Core.Interface;
using Core.Models;

namespace Core.Services;

public class TheaterCoreService
{
    private IRepository<Theater> _theaterRepository;

    public TheaterCoreService(IRepository<Theater> theaterRepository)
    {
        _theaterRepository = theaterRepository;
    }

    public async Task<IEnumerable<Theater>> GetTheaters()
    {
        var theaters = await _theaterRepository.GetAsync();
        return theaters;
    }

    public async Task<Theater> GetTheaterById(int theaterId)
    {
        var theater = await _theaterRepository.GetByIdAsync(theaterId);
        if (theater == null)
        {
            return null;
        }
        return theater;
    }

    public async Task<Theater> CreateTheater(Theater theater)
    {
        var addedTheater = await _theaterRepository.CreateAsync(theater);

        return addedTheater;
    }

    public async Task<Theater> UpdateTheater(Theater theater)
    {
        var updatedTheater = await _theaterRepository.UpdateAsync(theater);
        return updatedTheater;
    }

    public async Task DeleteTheaterById(int theaterId)
    {
        await _theaterRepository.DeleteByIdAsync(theaterId);
    }
}
