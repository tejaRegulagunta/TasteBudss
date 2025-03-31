using Microsoft.AspNetCore.Mvc;
using TasteBuds.Models;
using TasteBuds.AppDbContext;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TasteBuds.Controllers
{
    public class SearchController : Controller
    {
        private readonly TasteBudsDbContext _context;

        public SearchController(TasteBudsDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Results(string query)
        {
            var products = await _context.product
                .Where(p => p.Name.Contains(query) || p.Description.Contains(query))
                .ToListAsync();

            return View(products);
        }
    }
}
