using Core.Services;
using TrananAPI.Service;
using TrananAPI.DTO;

namespace TrananAPI.Services;

public class TheaterService
{
    private readonly TheaterCoreService _theaterCoreService;

    public TheaterService(TheaterCoreService theaterCoreService)
    {
        _theaterCoreService = theaterCoreService;
    }

    public async Task<IEnumerable<TheaterDTO>> GetTheaters()
    {
        var theaters = await _theaterCoreService.GetTheaters();
        var theaterDTOs = theaters.Select(t => Mapper.GenerateTheaterDTO(t)).ToList();
        return theaterDTOs;
    }

    public async Task<TheaterDTO> GetTheaterById(int theaterId)
    {
        var theater = await _theaterCoreService.GetTheaterById(theaterId);
        if (theater == null)
        {
            return null;
        }
        return Mapper.GenerateTheaterDTO(theater);
    }

    public async Task<TheaterDTO> CreateTheater(TheaterDTO theaterDTO)
    {
        var newTheater = Mapper.GenerateTheater(theaterDTO);
        var addedTheater = await _theaterCoreService.CreateTheater(newTheater);

        return Mapper.GenerateTheaterDTO(addedTheater);
    }

    public async Task<TheaterDTO> UpdateTheater(TheaterDTO theaterDTO)
    {
        var theaterToUpdate = Mapper.GenerateTheater(theaterDTO);
        var updatedTheater = await _theaterCoreService.UpdateTheater(theaterToUpdate);
        return Mapper.GenerateTheaterDTO(updatedTheater);
    }

    public async Task DeleteTheaterById(int theaterId)
    {
        await _theaterCoreService.DeleteTheaterById(theaterId);
    }
}
