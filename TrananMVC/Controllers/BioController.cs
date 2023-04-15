using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Service;
using TrananMVC.Interface;

namespace TrananMVC.Controllers;

public class BioController : Controller
{
    private readonly MovieScreeningService _movieScreeningService;
    private readonly IMovieService _movieService;

    public BioController(MovieScreeningService movieScreeningService, IMovieService movieService)
    {
        _movieScreeningService = movieScreeningService;
        _movieService = movieService;
    }

    public async Task<IActionResult> Index()
    {
        var upcomingMovies = await _movieService.GetUpcomingMoviesAsync();
        return View(upcomingMovies);
    }

    public async Task<IActionResult> AboutUs()
    {
        return View();
    }

    public async Task<IActionResult> Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
