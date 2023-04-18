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
        try
        {
            var upcomingMovies = await _movieService.GetUpcomingMoviesAsync();
            if (upcomingMovies == null)
            {
                return View(new List<MovieViewModel>());
            }
            return View(upcomingMovies);
        }
        catch (Exception)
        {
            return RedirectToAction(
                "Error",
                "Bio",
                new MessageViewModel(
                    "Ursäkta men sidan du söker kan inte hittas just nu. Tryck för att prova igen.",
                    "/bio/index",
                    "/bio/index"
                )
            );
        }
    }

    public async Task<IActionResult> AboutUs()
    {
        return View();
    }

    public async Task<IActionResult> Contact()
    {
        return View();
    }

    public async Task<IActionResult> Error(MessageViewModel messageViewModel)
    {
        return View(messageViewModel);
    }
}
