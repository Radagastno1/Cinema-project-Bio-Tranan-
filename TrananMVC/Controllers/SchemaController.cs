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
    //visar schemat för kommande visningar, där man kan välja en knapp
    // - reservera och komma till reservation controller
    public async Task<IActionResult> Index()
    {
        return View();
    }

}
