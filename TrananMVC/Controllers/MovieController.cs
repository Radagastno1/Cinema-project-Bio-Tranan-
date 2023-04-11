using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Service;

namespace TrananMVC.Controllers;

public class MovieController : Controller
{
    private readonly MovieService _movieService;

    public MovieController(MovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Get(int movieId)
    // {
    //     var movie = await _movieService.GetMovieById(movieId);
    //     if(movie == null)
    //     {
    //         return RedirectToAction("Index", "Movie");
    //     }
    //     return RedirectToAction("About", "Movie", new { MovieViewModel = movie });
    // }

    public async Task<IActionResult> About(int movieId)
    {
        var movieViewModel = await _movieService.GetMovieById(movieId);
        return View(movieViewModel);
    }
}
