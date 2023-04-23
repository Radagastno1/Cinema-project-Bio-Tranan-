using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Service;
using Core.Models;

namespace TrananMVC.Controllers;

public class BioController : Controller
{
    private readonly Core.Interface.IService<MovieScreening> _coreScreeningService;
    private readonly Core.Interface.IService<Movie> _coreMovieService;

    public BioController(
        Core.Interface.IService<MovieScreening> coreScreeningService,
        Core.Interface.IService<Movie> coreMovieService
    )
    {
        _coreMovieService = coreMovieService;
        _coreScreeningService = coreScreeningService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var movies = await _coreMovieService.Get();

            var upcomingMovies = movies.Where(m => m.AmountOfScreenings < m.MaxScreenings);
            var upcomingMoviesAsViewModels = movies
                .Select(m => Mapper.GenerateMovieAsViewModel(m))
                .ToList();

            if (upcomingMovies == null)
            {
                return View(new List<MovieViewModel>());
            }
            return View(upcomingMoviesAsViewModels);
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
