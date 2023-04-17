using Core.Models;
using TrananAPI.Service.Mapper;
using TrananAPI.DTO;
using TrananAPI.Interface;

namespace TrananAPI.Services;

public class TheaterService : IService<TheaterDTO, TheaterDTO>
{
    private readonly Core.Interface.IService<Theater> _coreTheaterService;

    public TheaterService(Core.Interface.IService<Theater> coreTheaterService)
    {
        _coreTheaterService = coreTheaterService;
    }

    public async Task<IEnumerable<TheaterDTO>> Get()
    {
        try
        {
       var theaters = await _coreTheaterService.Get();
        var theaterDTOs = theaters.Select(t => Mapper.GenerateTheaterDTO(t)).ToList();
        return theaterDTOs;
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
 
    }

    public async Task<TheaterDTO> GetById(int id)
    {
        var theater = await _coreTheaterService.GetById(id);
        if (theater == null)
        {
            return null;
        }
        return Mapper.GenerateTheaterDTO(theater);
    }

    public async Task<TheaterDTO> Create(TheaterDTO theaterDTO)
    {
        var newTheater = Mapper.GenerateTheater(theaterDTO);
        var addedTheater = await _coreTheaterService.Create(newTheater);

        return Mapper.GenerateTheaterDTO(addedTheater);
    }

    public async Task<TheaterDTO> Update(TheaterDTO theaterDTO)
    {
        var theaterToUpdate = Mapper.GenerateTheater(theaterDTO);
        var updatedTheater = await _coreTheaterService.Update(theaterToUpdate);
        return Mapper.GenerateTheaterDTO(updatedTheater);
    }

    public async Task DeleteById(int id)
    {
        await _coreTheaterService.DeleteById(id);
    }
}
