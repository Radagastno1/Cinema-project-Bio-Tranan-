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
        return await _movieScreeningRepository.GetUpcomingMovieScreenings();
    }
}