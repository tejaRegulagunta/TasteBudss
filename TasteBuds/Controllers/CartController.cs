// File: Controllers/CartController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasteBuds.AppDbContext;
using TasteBuds.Models;

namespace TasteBuds.Controllers
{
    [Authorize(Policy = "User")]
    public class CartController : Controller
    {
        private readonly TasteBudsDbContext _context;
        public CartController(TasteBudsDbContext context)
        {
            _context = context;
        }
        // Add item to cart or update quantity
        public async Task<IActionResult> AddCart(int id, string name, decimal price, int quantity = 1)
        {
            var cartItem = await _context.carts.FirstOrDefaultAsync(c => c.Name == name && c.UserId == id);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                CartModel cart = new CartModel
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    UserId = id
                };
                await _context.carts.AddAsync(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Remove item or decrease quantity
        public async Task<IActionResult> RemoveCart(int id)
        {
            var cart = await _context.carts.FirstOrDefaultAsync(c => c.id == id);
            if (cart != null)
            {
                if (cart.Quantity > 1)
                {
                    cart.Quantity--;
                }
                else
                {
                    _context.carts.Remove(cart);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // Display cart items
        public async Task<IActionResult> Index()
        {
            var cartItems = await _context.carts.ToListAsync();
            return View(cartItems);
        }
    }
}
