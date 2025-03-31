using Microsoft.AspNetCore.Mvc;
using TasteBuds.AppDbContext;
using TasteBuds.Models;

namespace TasteBuds.Controllers
{
    public class DeliveryAndPaymentController : Controller
    {

        private readonly TasteBudsDbContext _context;


        public DeliveryAndPaymentController(TasteBudsDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddDeliveryAndPayment()
        {
            return View(new Delivery());
        }
        //Http post for exploreMenu
        [HttpPost]
        public IActionResult AddDeliveryAndPayment(Delivery delivery)
        {
            return View();
        }
        public IActionResult Orders()
        {
            return View(_context.carts.ToList());
        }
        //Http post for exploreMenu
        [HttpPost]
        public IActionResult Orders(CartModel Cartorders)
        {
            return View();
        }
        public IActionResult OrdersConfirmedSatus()
        {
            return View(_context.carts.ToList());
        }
        [HttpPost]
        public IActionResult OrdersConfirmedSatus(CartModel Cartorders)
        {
            return View();
        }
    }
}
