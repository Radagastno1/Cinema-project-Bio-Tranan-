using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Interface;

namespace TrananMVC.Controllers;

public class MovieController : Controller
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    public async Task<IActionResult> Details(int movieId)
    {
        var movieViewModel = await _movieService.GetMovieByIdAsync(movieId);
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
    //  public async Task<IActionResult> TopMovies()
    // {
    //     var movies = await _movieService.GetAllMovies();
    //     var topMovies = movies.OrderByDescending(m => m.Rating);
    //     return View(topMovies);
    // }

}
