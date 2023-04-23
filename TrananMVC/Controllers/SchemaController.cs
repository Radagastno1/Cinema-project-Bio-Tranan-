using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrananMVC.ViewModel;
using TrananMVC.Interface;
using TrananMVC.Service;
using Core.Models;

namespace TrananMVC.Controllers;

public class SchemaController : Controller
{
    private readonly Core.Interface.IService<MovieScreening> _coreScreeningService;
    private readonly Core.Interface.IMovieScreeningService _coreScreeningService2;

    public SchemaController(Core.Interface.IService<MovieScreening> coreScreeningService,
    Core.Interface.IMovieScreeningService coreScreeningService2, 
    Core.Interface.IService<Movie> coreMovieService
    )
    {
        _coreScreeningService = coreScreeningService;
        _coreScreeningService2 = coreScreeningService2;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var movieScreeningViewModels = await _coreScreeningService.Get();
            if (movieScreeningViewModels == null)
            {
                return View(new List<MovieScreeningViewModel>());
            }
            return View(movieScreeningViewModels.Select(m => Mapper.GenerateMovieScreeningToViewModel(m)).ToList());
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
            var shownMovieScreenings = await _coreScreeningService2.GetShownScreenings();
            if (shownMovieScreenings == null)
            {
                return View(new List<MovieScreeningViewModel>());
            }
            var shownMovieScreeningsViewModels = shownMovieScreenings.Select(m => Mapper.GenerateMovieScreeningToViewModel(m)).ToList();
            return View(shownMovieScreeningsViewModels);
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
