using Core.Interface;
using Core.Models;

namespace Core.Services;

public class MovieScreeningService : IService<MovieScreening>, IMovieScreeningService
{
    private readonly IRepository<MovieScreening> _movieScreeningRepository;
    private readonly IMovieScreeningRepository _shownMovieScreeningRepository;
    private readonly IRepository<Theater> _theaterRepository;
    private readonly IRepository<Movie> _movieRepository;

    public MovieScreeningService(
        IRepository<MovieScreening> movieScreeningRepository,
        IRepository<Theater> theaterRepository,
        IRepository<Movie> movieRepository,
        IMovieScreeningRepository shownMovieScreeningRepository
    )
    {
        _movieScreeningRepository = movieScreeningRepository;
        _theaterRepository = theaterRepository;
        _movieRepository = movieRepository;
        _shownMovieScreeningRepository = shownMovieScreeningRepository;
    }

    public async Task<IEnumerable<MovieScreening>> Get()
    {
        try
        {
            var screenings = await _movieScreeningRepository.GetAsync();
            if(screenings == null)
            {
                return Enumerable.Empty<MovieScreening>();
            }
            return screenings;
        }
        catch (Exception)
        {
            throw new Exception("Failed getting movie screenings.");
        }
    }

    public async Task<IEnumerable<MovieScreening>> GetShownScreenings()
    {
        try
        {
            var shownScreenings = await _shownMovieScreeningRepository.GetShownAsync();
            if (shownScreenings == null)
            {
                return Enumerable.Empty<MovieScreening>();
            }
            return shownScreenings;
        }
        catch (Exception)
        {
            throw new Exception("Failed getting shown movie screenings.");
        }
    }

    public async Task<MovieScreening> GetById(int movieScreeningId)
    {
        try
        {
            var screening = await _movieScreeningRepository.GetByIdAsync(movieScreeningId);
            if (screening == null)
            {
                return new MovieScreening();
            }
            return screening;
        }
        catch (NullReferenceException)
        {
            throw new Exception("Failed getting movie screening by id.");
        }
    }

    public async Task<MovieScreening> Create(MovieScreening movieScreening)
    {
        try
        {
            var theater = await _theaterRepository.GetByIdAsync(movieScreening.TheaterId);
            var movie = await _movieRepository.GetByIdAsync(movieScreening.MovieId);
            movieScreening.PricePerPerson = movie.Price + theater.TheaterPrice;
            var addedMovieScreening = await _movieScreeningRepository.CreateAsync(movieScreening);
            if(addedMovieScreening == null)
            {
                return null;
            }
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

    public async Task<MovieScreening> Update(MovieScreening movieScreening)
    {
        try
        {
            var updatedMovieScreening = await _movieScreeningRepository.UpdateAsync(movieScreening);
            if(updatedMovieScreening == null)
            {
                return new MovieScreening();
            }
            return updatedMovieScreening;
        }
        catch (Exception)
        {
            throw new Exception("Something went wrong when updating movie screening.");
        }
    }

    public async Task DeleteById(int id)
    {
        try
        {
            await _movieScreeningRepository.DeleteByIdAsync(id);
        }
        catch (Exception)
        {
            throw new Exception("Something went wrong when deleting movie screening.");
        }
    }
}
