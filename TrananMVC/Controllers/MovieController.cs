using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Interface;
using TrananMVC.Service;

namespace TrananMVC.Controllers;

public class MovieController : Controller
{
    private readonly IMovieService _movieService;
    private readonly MovieScreeningService _movieScreeningService;

    public MovieController(IMovieService movieService, MovieScreeningService movieScreeningService)
    {
        _movieService = movieService;
        _movieScreeningService = movieScreeningService;
    }
    public async Task<IActionResult> Details(int movieId)
    {
        var movieViewModel = await _movieService.GetMovieByIdAsync(movieId);
        return View(movieViewModel);
    }

    public async Task<IActionResult> Review(int movieScreeningId)
    {
        var movieScreening = await _movieScreeningService.GetMovieScreeningById(movieScreeningId);
        var movie = await _movieService.GetMovieByIdAsync(movieScreening.MovieId);
        var reviewViewModel = new ReviewViewModel();
        reviewViewModel.MovieViewModel = movie;
        return View(reviewViewModel); 
    }
    
    [HttpPost]
    public async Task<IActionResult> PostRating(ReviewViewModel reviewViewModel)
    {
        // var createdReview = await 
        return View();
    }
    //  public async Task<IActionResult> TopMovies()
    // {
    //     var movies = await _movieService.GetAllMovies();
    //     var topMovies = movies.OrderByDescending(m => m.Rating);
    //     return View(topMovies);
    // }

}
