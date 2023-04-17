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
        var theaters = await _theaterRepository.GetAsync();
        return theaters;
    }

    public async Task<Theater> GetById(int theaterId)
    {
        var theater = await _theaterRepository.GetByIdAsync(theaterId);
        if (theater == null)
        {
            return null;
        }
        return theater;
    }

    public async Task<Theater> Create(Theater theater)
    {
        var addedTheater = await _theaterRepository.CreateAsync(theater);

        return addedTheater;
    }

    public async Task<Theater> Update(Theater theater)
    {
        var updatedTheater = await _theaterRepository.UpdateAsync(theater);
        return updatedTheater;
    }

    public async Task DeleteById(int theaterId)
    {
        await _theaterRepository.DeleteByIdAsync(theaterId);
    }
}
