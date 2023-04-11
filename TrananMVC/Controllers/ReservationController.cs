using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Service;

namespace TrananMVC.Controllers;

public class ReservationController : Controller
{


    public ReservationController()
    {

    }
    //sidan där man skapar reservationen
    public async Task<IActionResult> Create(int movieScreeningId)
    {
        return View();
    }
    //skickar reservationen som skapades till api
    [HttpPost]
    public async Task<IActionResult> PostReservation()
    {
        return View();
    }
    //visar sidan där man kan avboka sin reservation
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
