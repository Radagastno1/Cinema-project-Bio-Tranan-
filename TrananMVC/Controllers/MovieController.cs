using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Interface;
using TrananMVC.Service;

namespace TrananMVC.Controllers;

public class MovieController : Controller
{
    private readonly IMovieService _movieService;
    private readonly MovieScreeningService _movieScreeningService;
    private readonly ReviewService _reviewService;

    public MovieController(
        IMovieService movieService,
        MovieScreeningService movieScreeningService,
        ReviewService reviewService
    )
    {
        _movieService = movieService;
        _movieScreeningService = movieScreeningService;
        _reviewService = reviewService;
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
    public async Task<IActionResult> PostReview(ReviewViewModel reviewViewModel)
    {
        var movieViewModel = await _movieService.GetMovieByIdAsync(
            reviewViewModel.MovieViewModel.MovieId
        );
        reviewViewModel.MovieViewModel = movieViewModel;
        var createdReview = await _reviewService.CreateReview(reviewViewModel);

        return RedirectToAction(
            "Details",
            "Movie",
            new { movieId = reviewViewModel.MovieViewModel.MovieId }
        );
    }

    public async Task<IActionResult> TopMovies()
    {
        var movies = await _movieService.GetAllMoviesAsync();
        try
        {
            var topMovies = movies.OrderByDescending(m => m.Reviews.Max(r => r.Rating)).ToList();
            return View(topMovies);
        }
        catch (ArgumentNullException)
        {
            return View(new List<MovieViewModel>());
        }
    }
}
