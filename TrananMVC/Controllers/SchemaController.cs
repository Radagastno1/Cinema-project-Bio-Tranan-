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
        try
        {
            var movieScreeningViewModels = await _movieScreeningService.GetUpcomingScreenings();
            if (movieScreeningViewModels == null)
            {
                return View(new List<MovieScreeningViewModel>());
            }
            return View(movieScreeningViewModels);
        }
        catch (Exception)
        {
            return RedirectToAction(
                "Error",
                "Bio",
                new MessageViewModel("Ursäkta men sidan du söker kan inte hittas just nu. Prova igen eller gå till startsidan.", "/schema/index", "/bio/index")
            );
        }
    }

    public async Task<IActionResult> Shown()
    {
        try
        {
            var movieScreeningViewModels = await _movieScreeningService.GetShownScreenings();
            if (movieScreeningViewModels == null)
            {
                return View(new List<MovieScreeningViewModel>());
            }
            return View(movieScreeningViewModels);
        }
        catch (Exception)
        {
           return RedirectToAction(
                "Error",
                "Bio",
                new MessageViewModel("Ursäkta men sidan du söker kan inte hittas just nu. Prova igen eller gå till startsidan.", "/schema/shown", "/bio/index")
            );
        }
    }
}
