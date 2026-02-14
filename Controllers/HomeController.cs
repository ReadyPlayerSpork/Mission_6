using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission_6.Models;

// Controllers handle incoming web requests and decide what to send back (usually a View = a page).
// The "Home" controller typically serves the main pages of the site: home page, about, etc.
namespace Mission_6.Controllers;

/// <summary>
/// Handles requests for the main site pages: Home, Get to Know Joel, and the Error page.
/// </summary>
public class HomeController : Controller
{
    // Index is the default action. When someone visits the site root (e.g. https://localhost:5001/), they get this page.
    // IActionResult is the return type for controller actions; it can be a View, a redirect, JSON, etc.
    public IActionResult Index()
    {
        // Return View() looks for a view file that matches the action name. So it looks for Views/Home/Index.cshtml.
        return View();
    }

    // This action shows the "Get to Know Joel" page with links to his comedy and podcast.
    public IActionResult About()
    {
        return View();
    }

    // This runs when an error occurs. We pass an ErrorViewModel so the view can show the request ID.
    // [ResponseCache(...)] tells the browser not to cache this page, so each error shows fresh information.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        // Activity.Current?.Id gets the current request's ID for tracing. If it's null, we use HttpContext.TraceIdentifier instead.
        // The ?? is the "null-coalescing" operator: use the right side only if the left side is null.
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
