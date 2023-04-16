using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrananMVC.ViewModel;
using TrananMVC.Service;

namespace TrananMVC.Controllers;

public class SchemaController : Controller
{
    private readonly MovieScreeningService _movieScreeningService;

    public SchemaController(MovieScreeningService movieScreeningService)
    {
        _movieScreeningService = movieScreeningService;
    }

    public async Task<IActionResult> Index()
    {
        var movieScreeningViewModels = await _movieScreeningService.GetUpcomingScreenings();
        return View(movieScreeningViewModels);
    }

    public async Task<IActionResult> Shown()
    {
        var movieScreeningViewModels = await _movieScreeningService.GetShownScreenings();
        return View(movieScreeningViewModels);
    }
}
