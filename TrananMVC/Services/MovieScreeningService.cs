using Core.Interface;
using Core.Models;
using TrananMVC.ViewModel;

namespace TrananMVC.Service;

public class MovieScreeningService
{
    private readonly IService<MovieScreening> _coreScreeningService;
    private readonly IMovieScreeningService _coreScreeningService2;
    private readonly IService<Movie> _coreMovieService;

    public MovieScreeningService(
        IService<MovieScreening> coreScreeningService,
        IMovieScreeningService coreScreeningService2,
        IService<Movie> coreMovieService
    )
    {
        _coreScreeningService = coreScreeningService;
        _coreScreeningService2 = coreScreeningService2;
        _coreMovieService = coreMovieService;
    }

    public async Task<List<MovieScreeningViewModel>> GetUpcomingScreenings()
    {
        try
        {
            var movieScreenings = await _coreScreeningService.Get();
            if (movieScreenings == null)
            {
                return new List<MovieScreeningViewModel>();
            }
            return movieScreenings
                .Select(m => Mapper.GenerateMovieScreeningToViewModel(m))
                .ToList();
        }
        catch (Exception)
        {
            return new List<MovieScreeningViewModel>();
        }
    }

    public async Task<List<MovieScreeningViewModel>> GetShownScreenings()
    {
        try
        {
            var movieScreenings = await _coreScreeningService2.GetShownScreenings();
            if (movieScreenings == null)
            {
                return new List<MovieScreeningViewModel>();
            }
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
            var movieScreening = await _coreScreeningService.GetById(movieScreeningId);
            if (movieScreening == null)
            {
                return new MovieScreeningViewModel();
            }
            return Mapper.GenerateMovieScreeningToViewModel(movieScreening);
        }
        catch (Exception)
        {
            return new MovieScreeningViewModel();
        }
    }
}
