using TrananAPI.Service;
using TrananAPI.DTO;
using Core.Services;
using Core.Models;

namespace TrananAPI.Services;

public class MovieScreeningService
{
    private MovieScreeningCoreService _movieScreeningCoreService;
    public MovieScreeningService(MovieScreeningCoreService movieScreeningCoreService)
    {
        _movieScreeningCoreService = movieScreeningCoreService;
    }

    public async Task<IEnumerable<MovieScreeningOutgoingDTO>> GetUpcomingScreenings()
    {
        var screenings = await _movieScreeningCoreService.GetUpcomingScreenings();
        var screeningsDTOSs = screenings
            .Select(s => Mapper.GenerateMovieScreeningOutcomingDTO(s))
            .ToList();
        return screeningsDTOSs;
    }

    public async Task<MovieScreeningOutgoingDTO> GetMovieScreeningById(int movieScreeningId)
    {
        var screening = await _movieScreeningCoreService.GetMovieScreeningById(movieScreeningId);
        if (screening == null)
        {
            return new MovieScreeningOutgoingDTO();
        }
        return Mapper.GenerateMovieScreeningOutcomingDTO(screening);
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
            var addedMovieScreening = await _movieScreeningCoreService.CreateMovieScreening(
                newMovieScreening
            );
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
        var movieScreeningToUpdate = new Core.Models.MovieScreening()
        {
            MovieScreeningId = movieScreeningIncomingDTO.MovieScreeningId,
            DateAndTime = movieScreeningIncomingDTO.DateAndTime,
            MovieId = movieScreeningIncomingDTO.MovieId,
            TheaterId = movieScreeningIncomingDTO.TheaterId
        };
        var updatedMovieScreening = await _movieScreeningCoreService.UpdateMovieScreening(
            movieScreeningToUpdate
        );
        return Mapper.GenerateMovieScreeningOutcomingDTO(updatedMovieScreening);
    }

    public async Task DeleteMovieScreeningById(int id)
    {
       await _movieScreeningCoreService.DeleteMovieScreeningById(id);
    }
}
