using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Service;

namespace TrananMVC.Controllers;

public class HomeController : Controller
{
    private readonly MovieScreeningService _movieScreeningService;

    public HomeController(MovieScreeningService movieScreeningService)
    {
        _movieScreeningService = movieScreeningService;
    }

    public async Task<IActionResult> Index()
    {
        var movieScreeningViewModel = await _movieScreeningService.GetUpcomingScreenings();
        var distinctMovieScreenings = movieScreeningViewModel
            .GroupBy(m => m.MovieId)
            .Select(g => g.First())
            .ToList();
        return View(distinctMovieScreenings);
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
