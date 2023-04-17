using Core.Interface;
using Core.Models;
using TrananAPI.Service;
using TrananAPI.DTO;

namespace TrananAPI.Services;

public class TheaterService
{
    private readonly IService<Theater> _coreTheaterService;

    public TheaterService(IService<Theater> coreTheaterService)
    {
        _coreTheaterService = coreTheaterService;
    }

    public async Task<IEnumerable<TheaterDTO>> GetTheaters()
    {
        var theaters = await _coreTheaterService.Get();
        var theaterDTOs = theaters.Select(t => Mapper.GenerateTheaterDTO(t)).ToList();
        return theaterDTOs;
    }

    public async Task<TheaterDTO> GetTheaterById(int theaterId)
    {
        var theater = await _coreTheaterService.GetById(theaterId);
        if (theater == null)
        {
            return null;
        }
        return Mapper.GenerateTheaterDTO(theater);
    }

    public async Task<TheaterDTO> CreateTheater(TheaterDTO theaterDTO)
    {
        var newTheater = Mapper.GenerateTheater(theaterDTO);
        var addedTheater = await _coreTheaterService.Create(newTheater);

        return Mapper.GenerateTheaterDTO(addedTheater);
    }

    public async Task<TheaterDTO> UpdateTheater(TheaterDTO theaterDTO)
    {
        var theaterToUpdate = Mapper.GenerateTheater(theaterDTO);
        var updatedTheater = await _coreTheaterService.Update(theaterToUpdate);
        return Mapper.GenerateTheaterDTO(updatedTheater);
    }

    public async Task DeleteTheaterById(int theaterId)
    {
        await _coreTheaterService.DeleteById(theaterId);
    }
}
