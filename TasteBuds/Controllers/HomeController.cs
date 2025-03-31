using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TasteBuds.AppDbContext;
using TasteBuds.Models;

namespace TasteBuds.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TasteBudsDbContext _context;
    public HomeController(ILogger<HomeController> logger, TasteBudsDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.product.ToList());
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



    ////Get Menu
    //public IActionResult AddExploreMenu()
    //{
    //    return View(new ExploreMenu());
    //}


    //Http post for exploreMenu

    //[HttpPost]
    //public IActionResult AddExploreMenu(ExploreMenu menu)
    //{
    //    return View();
    //}









}
