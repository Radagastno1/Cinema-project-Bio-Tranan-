using Core.Data.Repository;
using Core.Models;

namespace Core.Services;

public class MovieScreeningCoreService
{
    private MovieScreeningRepository _movieScreeningsRepository;

    public MovieScreeningCoreService(MovieScreeningRepository movieScreeningRepository)
    {
        _movieScreeningsRepository = movieScreeningRepository;
    }

    public async Task<IEnumerable<MovieScreening>> GetUpcomingScreenings()
    {
        var screenings = await _movieScreeningsRepository.GetUpcomingScreenings();
        return screenings;
    }

    public async Task<MovieScreening> GetMovieScreeningById(int movieScreeningId)
    {
        var screening = await _movieScreeningsRepository.GetMovieScreeningById(movieScreeningId);
        if (screening == null)
        {
            return new MovieScreening();
        }
        return screening;
    }

    public async Task<MovieScreening> CreateMovieScreening(MovieScreening movieScreening)
    {
        try
        {
            var addedMovieScreening = await _movieScreeningsRepository.CreateMovieScreening(
                movieScreening
            );
            return addedMovieScreening;
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

    public async Task<MovieScreening> UpdateMovieScreening(
        MovieScreening movieScreening
    )
    {
        var updatedMovieScreening = await _movieScreeningsRepository.UpdateMovieScreening(
            movieScreening
        );
        return updatedMovieScreening;
    }

    public async Task DeleteMovieScreeningById(int id)
    {
        await _movieScreeningsRepository.DeleteMovieScreeningById(id);
    }
}
