using Core.Interface;
using Core.Models;

namespace Core.Services;

public class MovieScreeningCoreService
{
    private readonly IRepository<MovieScreening> _movieScreeningRepository;
    private readonly IRepository<Theater> _theaterRepository;
    private readonly IRepository<Movie> _movieRepository;

    public MovieScreeningCoreService(
        IRepository<MovieScreening> movieScreeningRepository,
        IRepository<Theater> theaterRepository,
        IRepository<Movie> movieRepository
    )
    {
        _movieScreeningRepository = movieScreeningRepository;
        _theaterRepository = theaterRepository;
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<MovieScreening>> GetUpcomingScreenings()
    {
        var screenings = await _movieScreeningRepository.GetAsync();
        return screenings;
    }

    public async Task<MovieScreening> GetMovieScreeningById(int movieScreeningId)
    {
        var screening = await _movieScreeningRepository.GetByIdAsync(movieScreeningId);
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
            var theater = await _theaterRepository.GetByIdAsync(movieScreening.TheaterId);
            var movie = await _movieRepository.GetByIdAsync(movieScreening.MovieId);
            movieScreening.PricePerPerson = movie.Price + theater.TheaterPrice;
            var addedMovieScreening = await _movieScreeningRepository.CreateAsync(movieScreening);
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

    public async Task<MovieScreening> UpdateMovieScreening(MovieScreening movieScreening)
    {
        var updatedMovieScreening = await _movieScreeningRepository.UpdateAsync(movieScreening);
        return updatedMovieScreening;
    }

    public async Task DeleteMovieScreeningById(int id)
    {
        await _movieScreeningRepository.DeleteByIdAsync(id);
    }
}
