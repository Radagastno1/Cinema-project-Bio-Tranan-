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

        var createReservationViewModel = new CreateReservationViewModel()
        {
            MovieScreeningViewModel = movieScreeningViewModel,
            ReservationViewModel = reservationViewModel
        };
        return View(createReservationViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> PostReservation(
        CreateReservationViewModel createReservationViewModel
    )
    {
        Console.WriteLine("post reservation anropas");
        var reservationMade = await _reservationService.CreateReservation(
            createReservationViewModel.ReservationViewModel
        );
        return RedirectToAction("ShowReservation", "Reservation", reservationMade);
    }

    public async Task<IActionResult> ShowReservation(ReservationViewModel reservationViewModel)
    {
        return View(reservationViewModel);
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
