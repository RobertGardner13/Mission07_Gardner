using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission06_Gardner.Models;
using Microsoft.EntityFrameworkCore;

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
        var categories = _context.Categories.OrderBy(c => c.CategoryName).ToList(); // Get all categories
        ViewBag.Categories = categories; // Store them in ViewBag

        return View("AddFilm", new Movie());
    }

    [HttpPost]
    public IActionResult AddFilm(Movie response)
    {
        try
        {
            _context.Movies.Add(response);
            _context.SaveChanges();

            // After successful add, redirect to confirmation or ShowMovies
            return RedirectToAction("ShowMovies");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "An error occurred while saving changes to the database.");
            TempData["ErrorMessage"] = "An error occurred while saving changes.";
            return RedirectToAction("Index");
        }
    }


    [HttpGet]
    public IActionResult ShowMovies()
    {
        var movies = _context.Movies
            .Include(m => m.Category)
            .OrderBy(x => x.Title).ToList();
        
        return View("ShowMovies", movies);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var record = _context.Movies
            .Include(m => m.Category) // Ensure you include the related Category
            .SingleOrDefault(x => x.MovieId == id);

        if (record == null)
        {
            return NotFound();
        }

        ViewBag.Categories = _context.Categories.OrderBy(c => c.CategoryName).ToList();
        return View("AddFilm", record);
    }

    [HttpPost]
    public IActionResult Edit(Movie updatedInfo)
    {
        var movie = _context.Movies.Find(updatedInfo.MovieId);
        if (movie == null)
        {
            return NotFound();
        }

        movie.Title = updatedInfo.Title;
        movie.Year = updatedInfo.Year;
        movie.Edited = updatedInfo.Edited;
        movie.CopiedToPlex = updatedInfo.CopiedToPlex;
        movie.Director = updatedInfo.Director;
        movie.Rating = updatedInfo.Rating;
        movie.LentTo = updatedInfo.LentTo;
        movie.Notes = updatedInfo.Notes;
        movie.CategoryID = updatedInfo.CategoryID;
        
        _context.Movies.Update(movie);
        _context.SaveChanges();
        return RedirectToAction("ShowMovies");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var recordToDelete = _context.Movies.Single(x => x.MovieId == id);
        return View("Delete", recordToDelete);
    }

    [HttpPost]
    public IActionResult Delete(Movie movie)
    {
        var movieToDelete = _context.Movies.Find(movie.MovieId);
        if (movieToDelete == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movieToDelete);
        _context.SaveChanges();
        
        return RedirectToAction("ShowMovies");
    }
} 


    