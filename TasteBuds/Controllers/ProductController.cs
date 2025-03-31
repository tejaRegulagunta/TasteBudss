using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasteBuds.AppDbContext;
using TasteBuds.Models;
using System.Collections.Generic;

namespace TasteBuds.Controllers
{
    public class ProductController : Controller
    {
        private readonly TasteBudsDbContext _context;
        public ProductController(TasteBudsDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.product.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }
        // GET: Product
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.product.ToListAsync());
        }

        // GET: Product/Details/
        // GET: Product/Create
        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        // POST: Product/Create
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Description,ImagePath")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        // GET: Product/Edit/
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.product.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }
        // POST: Product/Edit/
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,ImagePath")] Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        // GET: Product/Delete/
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.product.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
        // POST: Product/Delete/
        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.product.FindAsync(id);
            if (product != null)
            {
                _context.product.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}