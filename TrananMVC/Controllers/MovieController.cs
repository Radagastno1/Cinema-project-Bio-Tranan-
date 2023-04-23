using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Interface;
using Core.Models;
using TrananMVC.Service;

namespace TrananMVC.Controllers;

public class MovieController : Controller
{
    private readonly Core.Interface.IService<Movie> _coreMovieService;
    private readonly Core.Interface.IService<MovieScreening> _coreScreeningService;
     private readonly Core.Interface.IService<Review> _coreReviewService;

    public MovieController(
        Core.Interface.IService<Movie> coreMovieService,
        Core.Interface.IService<MovieScreening> coreScreeningService,
        Core.Interface.IService<Review> coreReviewService
    )
    {
        _coreMovieService = coreMovieService;
        _coreScreeningService = coreScreeningService;
        _coreReviewService = coreReviewService;
    }

    public async Task<IActionResult> Details(int movieId)
    {
        try
        {
            var movie = await _coreMovieService.GetById(movieId);
            var movieViewModel = Mapper.GenerateMovieAsViewModel(movie);
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
            var movieScreening = await _coreScreeningService.GetById(
                movieScreeningId
            );
            var movie = await _coreMovieService.GetById(movieScreening.MovieId);
            var reviewViewModel = new ReviewViewModel();
            reviewViewModel.MovieViewModel = Mapper.GenerateMovieAsViewModel(movie);
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
            var movie = await _coreMovieService.GetById(
                reviewViewModel.MovieViewModelId
            );

            reviewViewModel.MovieViewModel = Mapper.GenerateMovieAsViewModel(movie);
            var createdReview = await _coreReviewService.Create(Mapper.GenerateReview(reviewViewModel));

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
            var movies = await _coreMovieService.Get();
            var topMovies = movies
                .Where(m => m.Reviews != null && m.Reviews.Any())
                .OrderByDescending(m => m.Reviews.Max(r => r.Rating))
                .Take(5)
                .ToList();
            return View(topMovies.Select(m => Mapper.GenerateMovieAsViewModel(m)).ToList());
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
