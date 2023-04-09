using TrananAPI.Data.Repository;
using TrananAPI.DTO;
using TrananAPI.Models;

namespace TrananAPI.Service;

public class TheaterService
{
    private TheaterRepository _theaterRepository;

    public TheaterService(TheaterRepository theaterRepository)
    {
        _theaterRepository = theaterRepository;
    }

    public async Task<IEnumerable<TheaterDTO>> GetTheaters()
    {
        var theaters = await _theaterRepository.GetTheaters();
        var theaterDTOs = theaters
            .Select(t => Mapper.GenerateTheaterDTO(t))
            .ToList();
        return theaterDTOs;
    }

    public async Task<TheaterDTO> GetTheaterById(int theaterId)
    {
        var theater = await _theaterRepository.GetTheaterById(theaterId);
        if (theater == null)
        {
            return null;
        }
        return Mapper.GenerateTheaterDTO(theater);
    }

    public async Task<TheaterDTO> CreateTheater(
        TheaterDTO theaterDTO
    )
    {
        var newTheater = Mapper.GenerateTheater(theaterDTO);
        var addedTheater = await _theaterRepository.CreateTheater(
            newTheater
        );

        return Mapper.GenerateTheaterDTO(addedTheater);
    }
    public async Task<TheaterDTO> UpdateTheater(TheaterDTO theaterDTO)
    {
        var theaterToUpdate = Mapper.GenerateTheater(theaterDTO);
        var updatedTheater = await _theaterRepository.UpdateTheater(theaterToUpdate);
        return Mapper.GenerateTheaterDTO(updatedTheater);
    }
    public async Task DeleteTheaterById(int theaterId)
    {
        await _theaterRepository.DeleteTheaterById(theaterId);
    }
}
