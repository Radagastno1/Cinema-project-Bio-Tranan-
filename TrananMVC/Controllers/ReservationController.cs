using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Interface;

namespace TrananMVC.Controllers;

public class ReservationController : Controller
{
    private readonly IMovieService<MovieScreeningViewModel> _movieScreeningService;
    private readonly IReservationService _reservationService;

    public ReservationController(
        IMovieService<MovieScreeningViewModel> movieScreeningService,
        IReservationService reservationService
    )
    {
        _movieScreeningService = movieScreeningService;
        _reservationService = reservationService;
    }

    public async Task<IActionResult> Create(int movieScreeningId)
    {
        try
        {
            var movieScreeningViewModel = await _movieScreeningService.GetById(
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
        catch (Exception)
        {
            return RedirectToAction(
                "Error",
                "Bio",
                new MessageViewModel(
                    "Ursäkta men sidan du söker kan inte hittas just nu. Prova igen eller gå till startsidan.",
                    "/reservation/create",
                    "/bio/index"
                )
            );
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostReservation(
        MovieScreeningReservationViewModel movieScreeningReservationViewModel
    )
    {
        try
        {
            var createdReservationViewModel = await _reservationService.CreateReservation(
                movieScreeningReservationViewModel.ReservationViewModel
            );
            return RedirectToAction("ShowReservation", "Reservation", createdReservationViewModel);
        }
        catch (Exception)
        {
            return RedirectToAction(
                "Error",
                "Bio",
                new MessageViewModel(
                    "Ursäkta men sidan du söker kan inte hittas just nu. Prova igen eller gå till startsidan.",
                    "/reservation/postreservation",
                    "/bio/index"
                )
            );
        }
    }

    public async Task<IActionResult> ShowReservation(
        CreatedReservationViewModel createdReservationViewModel
    )
    {
        return View(createdReservationViewModel);
    }

    public async Task<IActionResult> Cancel(int reservationId)
    {
        try
        {
            var isDeleted = await _reservationService.DeleteReservationById(reservationId);
            if (isDeleted)
            {
                return View(); 
            }
            return RedirectToAction("ShowReservation", "Reservation");
        }
        catch (Exception)
        {
            return RedirectToAction(
                "Error",
                "Bio",
                new MessageViewModel(
                    "Ursäkta men sidan du söker kan inte hittas just nu. Prova igen eller gå till startsidan.",
                    "/reservation/postreservation",
                    "/bio/index"
                )
            );
        }
    }
}
