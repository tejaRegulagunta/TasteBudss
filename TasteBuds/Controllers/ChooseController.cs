using Microsoft.AspNetCore.Mvc;
using TasteBuds.Models;

namespace TasteBuds.Controllers
{
    public class ChooseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult AddChooseSection()
        {
            return View(new ChooseSection());
        }
        //Http post for addChoose

        [HttpPost]
        public IActionResult AddChooseSection(ChooseSection choose)
        {
            return View();
        }
    }
}
