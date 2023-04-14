using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Service;

namespace TrananMVC.Controllers;

public class ReservationController : Controller
{
    private readonly MovieScreeningService _movieScreeningService;
    private readonly ReservationService _reservationService;

    public ReservationController(
        MovieScreeningService movieScreeningService,
        ReservationService reservationService
    )
    {
        _movieScreeningService = movieScreeningService;
        _reservationService = reservationService;
    }

    public async Task<IActionResult> Create(int movieScreeningId)
    {
        var movieScreeningViewModel = await _movieScreeningService.GetMovieScreeningById(
            movieScreeningId
        );
        var reservationViewModel = new ReservationViewModel();
        reservationViewModel.MovieScreeningId = movieScreeningId;

        var movieScreeningReservationViewModel = new MovieScreeningReservationViewModel()
        {
            MovieScreeningViewModel = movieScreeningViewModel,
            ReservationViewModel = reservationViewModel
        };
        return View(movieScreeningReservationViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> PostReservation(
        MovieScreeningReservationViewModel movieScreeningReservationViewModel
    )
    {
        Console.WriteLine("post reservation anropas");
        var createdReservationViewModel = await _reservationService.CreateReservation(
            movieScreeningReservationViewModel.ReservationViewModel
        );
        return RedirectToAction("ShowReservation", "Reservation", createdReservationViewModel);
    }

    public async Task<IActionResult> ShowReservation(
        CreatedReservationViewModel createdReservationViewModel
    )
    {
        return View(createdReservationViewModel);
    }

    public async Task<IActionResult> Cancel()
    {
        return View();
    }

    //skickar en delete reservation till api
    [HttpDelete]
    public async Task<IActionResult> PostCancelledReservation()
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
