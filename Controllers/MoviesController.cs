using Microsoft.AspNetCore.Mvc;
using Mission_6.Data;
using Mission_6.Models;

// This controller handles everything related to the movie collection: viewing the list and adding new movies.
namespace Mission_6.Controllers;

/// <summary>
/// Handles requests for the movie collection: listing all movies and showing the form to add a new one.
/// Uses MovieDbContext to read and write data in the SQLite database.
/// </summary>
public class MoviesController : Controller
{
    // We store the database context in a private field so every action in this controller can use it.
    // "readonly" means we can only set it in the constructor; after that it cannot be changed.
    private readonly MovieDbContext _context;

    // This is the constructor. When ASP.NET Core needs to run an action on MoviesController, it creates a new
    // MoviesController and passes in a MovieDbContext. This is called "dependency injection": the framework
    // gives us the dependencies we need instead of us creating them ourselves.
    public MoviesController(MovieDbContext context)
    {
        _context = context;
    }

    // Index action: shows the list of all movies in the collection.
    // Called when the user goes to /Movies or clicks "View Collection" in the nav.
    public IActionResult Index()
    {
        // _context.Movies is the set of all Movie rows in the database.
        // .OrderBy(m => m.Title) sorts them alphabetically by title. "m" is each movie in the set.
        // .ToList() runs the query and puts the results into a list we can pass to the view.
        var movies = _context.Movies.OrderBy(m => m.Title).ToList();
        return View(movies);
    }

    // [HttpGet] means this method runs only when the request is a GET (e.g. the user navigates to the Add page).
    // When the user first opens "Add Movie", we show an empty form by passing a new Movie() with no data.
    [HttpGet]
    public IActionResult Add()
    {
        return View(new Movie());
    }

    // [HttpPost] means this method runs when the user submits the form (clicks "Add Movie").
    // The form sends the data back as a POST request. ASP.NET Core automatically fills the "movie" parameter
    // with the values from the form — this is called "model binding."
    [ValidateAntiForgeryToken]
    // This attribute helps prevent cross-site request forgery (CSRF) attacks. The form includes a hidden token that must match.
    [HttpPost]
    public IActionResult Add(Movie movie)
    {
        // ModelState holds any validation errors. If the user left required fields blank or broke a rule (e.g. Year out of range),
        // ModelState.IsValid will be false and we show the form again with error messages.
        if (ModelState.IsValid)
        {
            _context.Movies.Add(movie);   // Add the new movie to the in-memory list of movies.
            _context.SaveChanges();      // Write the change to the SQLite database file.
            // TempData lets us pass a one-time message to the next page. After redirecting, the View Collection page can show "Movie added!"
            TempData["SuccessMessage"] = $"\"{movie.Title}\" has been added to the collection.";
            return RedirectToAction(nameof(Index));  // Send the user to the View Collection page.
        }
        // If validation failed, return the same view with the movie object. The user's input is preserved and validation messages show.
        return View(movie);
    }
}
