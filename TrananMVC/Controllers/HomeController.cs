using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Service;

namespace TrananMVC.Controllers;

public class HomeController : Controller
{
    private readonly MovieScreeningService _movieScreeningService;
    private readonly MovieService _movieService;

    public HomeController(MovieScreeningService movieScreeningService, MovieService movieService)
    {
        _movieScreeningService = movieScreeningService;
        _movieService = movieService;
    }

    public async Task<IActionResult> Index()
    {
        var movieScreeningViewModel = await _movieScreeningService.GetUpcomingScreenings();
        return View(movieScreeningViewModel);
    }

    public async Task<IActionResult> Movies()
    {
        var moviesViewModel = await _movieService.GetAllMovies();
        return View(moviesViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
