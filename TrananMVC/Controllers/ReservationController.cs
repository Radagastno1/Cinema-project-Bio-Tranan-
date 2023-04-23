using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Interface;
using Core.Models;
using TrananMVC.Service;

namespace TrananMVC.Controllers;

public class ReservationController : Controller
{
    private readonly Core.Interface.IService<MovieScreening> _coreScreeningService;
    private readonly Core.Interface.IService<Reservation> _coreReservationService;
    private readonly Core.Interface.IService<Movie> _coreMovieService;

    public ReservationController(
        Core.Interface.IService<MovieScreening> coreScreeningService,
        Core.Interface.IService<Reservation> coreReservationService,
        Core.Interface.IService<Movie> coreMovieService
    )
    {
        _coreReservationService = coreReservationService;
        _coreScreeningService = coreScreeningService;
        _coreMovieService = coreMovieService;
    }

    public async Task<IActionResult> Create(int movieScreeningId)
    {
        try
        {
            var movieScreening = await _coreScreeningService.GetById(movieScreeningId);

            var movieScreeningViewModel = Mapper.GenerateMovieScreeningToViewModel(movieScreening);

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
            var reservationToCreate = Mapper.GenerateReservation(
                movieScreeningReservationViewModel.ReservationViewModel
            );
            var createdReservation = await _coreReservationService.Create(reservationToCreate);
            var movieScreening = await _coreScreeningService.GetById(
                createdReservation.MovieScreeningId
            );
            var movie = await _coreMovieService.GetById(movieScreening.MovieId);
            var createdReservationViewModel = Mapper.GenerateCreatedReservationViewModel(
                movieScreening,
                createdReservation,
                movie
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
            await _coreReservationService.DeleteById(reservationId);

            return View();
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
