using TrananAPI.Data.Repository;
using TrananAPI.DTO;
using TrananAPI.Models;

namespace TrananAPI.Service;

public class MovieScreeningService
{
    private MovieScreeningRepository _movieScreeningsRepository;

    public MovieScreeningService(MovieScreeningRepository movieScreeningRepository)
    {
        _movieScreeningsRepository = movieScreeningRepository;
        UpdateMaxScreeningsPerMovie();
    }

    public async Task<IEnumerable<MovieScreeningOutgoingDTO>> GetUpcomingScreenings()
    {
        var screenings = await _movieScreeningsRepository.GetUpcomingScreenings();
        var screeningsDTOSs = screenings
            .Select(s => Mapper.GenerateMovieScreeningOutcomingDTO(s))
            .ToList();
        return screeningsDTOSs;
    }

    public async Task<MovieScreeningOutgoingDTO> GetMovieScreeningById(int movieScreeningId)
    {
        var screening = await _movieScreeningsRepository.GetMovieScreeningById(movieScreeningId);
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
            var addedMovieScreening = await _movieScreeningsRepository.CreateMovieScreening(
                newMovieScreening
            );
            var addedMovieScreeningDTO = Mapper.GenerateMovieScreeningOutcomingDTO(
                addedMovieScreening
            );
            return addedMovieScreeningDTO;
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException();
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException();
        }
    }

    public async Task<MovieScreeningOutgoingDTO> UpdateMovieScreening(
        MovieScreeningIncomingDTO movieScreeningIncomingDTO
    )
    {
        var movieScreeningToUpdate = new MovieScreening()
        {
            MovieScreeningId = movieScreeningIncomingDTO.MovieScreeningId,
            DateAndTime = movieScreeningIncomingDTO.DateAndTime,
            MovieId = movieScreeningIncomingDTO.MovieId,
            TheaterId = movieScreeningIncomingDTO.TheaterId
        };
        var updatedMovieScreening = await _movieScreeningsRepository.UpdateMovieScreening(
            movieScreeningToUpdate
        );
        return Mapper.GenerateMovieScreeningOutcomingDTO(updatedMovieScreening);
    }

    public async Task DeleteMovieScreeningById(int id)
    {
        await _movieScreeningsRepository.DeleteMovieScreeningById(id);
    }

    private async Task UpdateMaxScreeningsPerMovie() //måste jag ha task ??
    {
        var upcomingMovieScreenings = await _movieScreeningsRepository.GetUpcomingScreenings();
        var movieScreeningsToday = upcomingMovieScreenings.Where(
            m => m.DateAndTime == DateTime.Today
        );
        if (movieScreeningsToday != null)
        {
            foreach (var screening in movieScreeningsToday)
            {
                screening.Movie.AmountOfScreenings++;
            }
        }
        await _movieScreeningsRepository.SaveChanges();
    }
}
