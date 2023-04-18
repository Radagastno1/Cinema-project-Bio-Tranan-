using TrananMVC.Interface;
using Core.Models;
using TrananMVC.ViewModel;

namespace TrananMVC.Service;

public class MovieScreeningService : IMovieService<MovieScreeningViewModel>
{
    private readonly Core.Interface.IService<MovieScreening> _coreScreeningService;
    private readonly Core.Interface.IMovieScreeningService _coreScreeningService2;
    private readonly Core.Interface.IService<Movie> _coreMovieService;

    public MovieScreeningService(
        Core.Interface.IService<MovieScreening> coreScreeningService,
        Core.Interface.IMovieScreeningService coreScreeningService2,
        Core.Interface.IService<Movie> coreMovieService
    )
    {
        _coreScreeningService = coreScreeningService;
        _coreScreeningService2 = coreScreeningService2;
        _coreMovieService = coreMovieService;
    }

    public async Task<List<MovieScreeningViewModel>> GetUpcoming()
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
    //Get all shown screenings
    public async Task<List<MovieScreeningViewModel>> GetAll()
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

    public async Task<MovieScreeningViewModel> GetById(int id)
    {
        try
        {
            var movieScreening = await _coreScreeningService.GetById(id);
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
