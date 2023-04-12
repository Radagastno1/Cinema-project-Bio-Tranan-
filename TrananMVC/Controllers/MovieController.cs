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

    //visar alla filmer
    public async Task<IActionResult> Index()
    {
        var moviesViewModel = await _movieService.GetAllMovies();
        return View(moviesViewModel);
    }

    //info om en film
    public async Task<IActionResult> Details(int movieId)
    {
        var movieViewModel = await _movieService.GetMovieById(movieId);
        return View(movieViewModel);
    }

    //visa sidan där man kan ge rate på movie
    public async Task<IActionResult> Rate()
    {
        return View();
    }

    //skickar ratingen till api
    [HttpPost]
    public async Task<IActionResult> PostRating()
    {
        return View();
    }

}