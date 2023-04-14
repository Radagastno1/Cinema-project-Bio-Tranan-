using Core.Data.Repository;
using Core.Models;

namespace Core.Services;

public class TheaterCoreService
{
    private TheaterRepository _theaterRepository;

    public TheaterCoreService(TheaterRepository theaterRepository)
    {
        _theaterRepository = theaterRepository;
    }

    public async Task<IEnumerable<Theater>> GetTheaters()
    {
        var theaters = await _theaterRepository.GetTheaters();
        return theaters;
    }

    public async Task<Theater> GetTheaterById(int theaterId)
    {
        var theater = await _theaterRepository.GetTheaterById(theaterId);
        if (theater == null)
        {
            return null;
        }
        return theater;
    }

    public async Task<Theater> CreateTheater(Theater theater)
    {
        var addedTheater = await _theaterRepository.CreateTheater(theater);

        return addedTheater;
    }

    public async Task<Theater> UpdateTheater(Theater theater)
    {
        var updatedTheater = await _theaterRepository.UpdateTheater(theater);
        return updatedTheater;
    }

    public async Task DeleteTheaterById(int theaterId)
    {
        await _theaterRepository.DeleteTheaterById(theaterId);
    }
}
