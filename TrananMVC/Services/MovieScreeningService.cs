using Core.Services;
using TrananMVC.ViewModel;

namespace TrananMVC.Service;

public class MovieScreeningService
{
    private readonly MovieScreeningCoreService _movieScreeningCoreService;
    private readonly MovieCoreService _movieCoreService;

    public MovieScreeningService(MovieScreeningCoreService movieScreeningCoreService,MovieCoreService movieCoreService)
    {
        _movieScreeningCoreService = movieScreeningCoreService;
        _movieCoreService = movieCoreService;
    }

    public async Task<List<MovieScreeningViewModel>> GetUpcomingScreenings()
    {
        try
        {
            var movieScreenings = await _movieScreeningCoreService.GetUpcomingScreenings();
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
            var movieScreening = await _movieScreeningCoreService.GetMovieScreeningById(movieScreeningId);
            return Mapper.GenerateMovieScreeningToViewModel(movieScreening);
        }
        catch (Exception)
        {
            return new MovieScreeningViewModel();
        }
    }
}
