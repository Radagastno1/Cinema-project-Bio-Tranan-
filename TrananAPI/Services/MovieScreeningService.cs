using TrananAPI.Service;
using TrananAPI.DTO;
using Core.Interface;
using Core.Models;

namespace TrananAPI.Services;

public class MovieScreeningService
{
    private readonly IService<MovieScreening> _coreScreeningService;

    public MovieScreeningService(IService<MovieScreening> coreScreeningService)
    {
        _coreScreeningService = coreScreeningService;
    }

    public async Task<IEnumerable<MovieScreeningOutgoingDTO>> GetUpcomingScreenings()
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

    public async Task<MovieScreeningOutgoingDTO> GetMovieScreeningById(int movieScreeningId)
    {
        try
        {
            var screening = await _coreScreeningService.GetById(movieScreeningId);
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

    public async Task<MovieScreeningOutgoingDTO> CreateMovieScreening(
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

    public async Task<MovieScreeningOutgoingDTO> UpdateMovieScreening(
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

    public async Task DeleteMovieScreeningById(int id)
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
