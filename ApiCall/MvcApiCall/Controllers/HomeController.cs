using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcApiCall.Models;
using Microsoft.Extensions.Configuration;

/* Read on Logger:
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-6.0
*/

namespace MvcApiCall.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly string _apikey;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _apikey = configuration["NYT"];
    }

    public IActionResult Index()
    {
        Task<string> allArticles = Article.GetArticles(_apikey);
        return View(allArticles);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
