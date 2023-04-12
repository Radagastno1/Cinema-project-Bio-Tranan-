using TrananMVC.Repository;
using TrananMVC.ViewModel;

namespace TrananMVC.Service;

public class MovieScreeningService
{
    private readonly MovieScreeningRepository _movieScreeningRepository;

    public MovieScreeningService(MovieScreeningRepository movieScreeningRepository)
    {
        _movieScreeningRepository = movieScreeningRepository;
    }

    public async Task<List<MovieScreeningViewModel>> GetUpcomingScreenings()
    {
        try
        {
            var movieScreenings = await _movieScreeningRepository.GetUpcomingMovieScreenings();
            return movieScreenings
                .Select(m => Mapper.GenerateMovieScreeningToViewModel(m))
                .ToList();
        }
        catch (Exception)
        {
            return new List<MovieScreeningViewModel>();
        }
    }

    public async Task<MovieScreeningViewModel> GetMovieScreeningById(int movieScreeningId)
    {
        try
        {
            var movieScreening = await _movieScreeningRepository.GetMovieScreeningById(movieScreeningId);
            return Mapper.GenerateMovieScreeningToViewModel(movieScreening);
        }
        catch (Exception)
        {
            return new MovieScreeningViewModel();
        }
    }
}
