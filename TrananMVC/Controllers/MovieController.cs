using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Interface;

namespace TrananMVC.Controllers;

public class MovieController : Controller
{
    private readonly IMovieService<MovieViewModel> _movieService;
    private readonly IMovieService<MovieScreeningViewModel> _movieScreeningService;
    private readonly IReviewService _reviewService;

    public MovieController(
        IMovieService<MovieViewModel> movieService,
        IMovieService<MovieScreeningViewModel> movieScreeningService,
        IReviewService reviewService
    )
    {
        _movieService = movieService;
        _movieScreeningService = movieScreeningService;
        _reviewService = reviewService;
    }

    public async Task<IActionResult> Details(int movieId)
    {
        try
        {
            var movieViewModel = await _movieService.GetById(movieId);
            return View(movieViewModel);
        }
        catch (Exception)
        {
            return RedirectToAction(
                "Error",
                "Bio",
                new MessageViewModel(
                    "Ursäkta men sidan du söker kan inte hittas just nu. Prova igen eller gå till startsidan.",
                    "/movie/details",
                    "/bio/index"
                )
            );
        }
    }

    public async Task<IActionResult> Review(int movieScreeningId)
    {
        try
        {
            var movieScreening = await _movieScreeningService.GetById(
                movieScreeningId
            );
            var movie = await _movieService.GetById(movieScreening.MovieId);
            var reviewViewModel = new ReviewViewModel();
            reviewViewModel.MovieViewModel = movie;
            return View(reviewViewModel);
        }
        catch (Exception)
        {
            return RedirectToAction(
                "Error",
                "Bio",
                new MessageViewModel(
                    "Ursäkta men sidan du söker kan inte hittas just nu. Prova igen eller gå till startsidan.",
                    "/movie/review",
                    "/bio/index"
                )
            );
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostReview(ReviewViewModel reviewViewModel)
    {
        try
        {
            var movieViewModel = await _movieService.GetById(
                reviewViewModel.MovieViewModelId
            );

            reviewViewModel.MovieViewModel = movieViewModel;
            var createdReview = await _reviewService.CreateReview(reviewViewModel);

            return RedirectToAction(
                "Details",
                "Movie",
                new { movieId = reviewViewModel.MovieViewModel.Id }
            );
        }
        catch (Exception)
        {
            return RedirectToAction(
                "Error",
                "Bio",
                new MessageViewModel(
                    "Ursäkta men sidan du söker kan inte hittas just nu. Prova igen eller gå till startsidan.",
                    "/movie/postreview",
                    "/bio/index"
                )
            );
        }
    }

    public async Task<IActionResult> TopMovies()
    {
        try
        {
            var movies = await _movieService.GetAll();
            var topMovies = movies
                .Where(m => m.Reviews != null && m.Reviews.Any())
                .OrderByDescending(m => m.Reviews.Max(r => r.Rating))
                .Take(5)
                .ToList();
            return View(topMovies);
        }
        catch (ArgumentNullException)
        {
            return View(new List<MovieViewModel>());
        }
        catch (Exception)
        {
            return RedirectToAction(
                "Error",
                "Bio",
                new MessageViewModel(
                    "Ursäkta men sidan du söker kan inte hittas just nu. Prova igen eller gå till startsidan.",
                    "/movie/topmovies",
                    "/bio/index"
                )
            );
        }
    }
}
