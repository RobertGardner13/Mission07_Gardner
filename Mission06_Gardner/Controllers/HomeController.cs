using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission06_Gardner.Models;

namespace Mission06_Gardner.Controllers;

public class HomeController : Controller
{
    private readonly MovieContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(MovieContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetToKnowJoel()
    {
        return View("GetToKnowJoel");
    }

    [HttpGet]
    public IActionResult AddFilm()
    {
        return View("AddFilm");
    }

    [HttpPost]
    public IActionResult AddFilm(NewMovie response)
    {
        _context.NewMovies.Add(response);
        _context.SaveChanges(); // Save changes to the database
        return View("Confirmation", response);
    }
} 