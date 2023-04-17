using TrananAPI.Interface;
using TrananAPI.Service.Mapper;
using TrananAPI.DTO;
using Core.Models;

namespace TrananAPI.Services;

public class MovieScreeningService : IService<MovieScreeningOutgoingDTO, MovieScreeningIncomingDTO>
{
    private readonly Core.Interface.IService<MovieScreening> _coreScreeningService;

    public MovieScreeningService(Core.Interface.IService<MovieScreening> coreScreeningService)
    {
        _coreScreeningService = coreScreeningService;
    }

    public async Task<IEnumerable<MovieScreeningOutgoingDTO>> Get()
    {
        try
        {
            var screenings = await _coreScreeningService.Get();
            var screeningsDTOSs = screenings
                .Select(s => Mapper.GenerateMovieScreeningOutcomingDTO(s))
                .ToList();
            return screeningsDTOSs;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<MovieScreeningOutgoingDTO> GetById(int id)
    {
        try
        {
            var screening = await _coreScreeningService.GetById(id);
            if (screening == null)
            {
                return new MovieScreeningOutgoingDTO();
            }
            return Mapper.GenerateMovieScreeningOutcomingDTO(screening);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<MovieScreeningOutgoingDTO> Create(
        MovieScreeningIncomingDTO movieScreeningIncomingDTO
    )
    {
        try
        {
            var newMovieScreening = new MovieScreening()
            {
                MovieId = movieScreeningIncomingDTO.MovieId,
                TheaterId = movieScreeningIncomingDTO.TheaterId,
                DateAndTime = movieScreeningIncomingDTO.DateAndTime
            };
            var addedMovieScreening = await _coreScreeningService.Create(newMovieScreening);
            var addedMovieScreeningDTO = Mapper.GenerateMovieScreeningOutcomingDTO(
                addedMovieScreening
            );
            return addedMovieScreeningDTO;
        }
        catch (NullReferenceException e)
        {
            throw new NullReferenceException(e.Message);
        }
        catch (InvalidOperationException e)
        {
            if (e.Message == "Theater not available at chosen time and day.")
            {
                throw new InvalidOperationException(e.Message);
            }
            else if (e.Message == "Movie has maximum amount moviescreenings.")
            {
                throw new InvalidOperationException(e.Message);
            }
            throw new InvalidOperationException();
        }
    }

    public async Task<MovieScreeningOutgoingDTO> Update(
        MovieScreeningIncomingDTO movieScreeningIncomingDTO
    )
    {
        try
        {
            var movieScreeningToUpdate = new Core.Models.MovieScreening()
            {
                MovieScreeningId = movieScreeningIncomingDTO.Id,
                DateAndTime = movieScreeningIncomingDTO.DateAndTime,
                MovieId = movieScreeningIncomingDTO.MovieId,
                TheaterId = movieScreeningIncomingDTO.TheaterId
            };
            var updatedMovieScreening = await _coreScreeningService.Update(movieScreeningToUpdate);
            return Mapper.GenerateMovieScreeningOutcomingDTO(updatedMovieScreening);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task DeleteById(int id)
    {
        try
        {
            await _coreScreeningService.DeleteById(id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
