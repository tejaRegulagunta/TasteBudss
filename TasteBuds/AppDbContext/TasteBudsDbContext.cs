using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TasteBuds.AppDbContext;
using TasteBuds.Models;

namespace TasteBuds.AppDbContext
{
    public class TasteBudsDbContext : IdentityDbContext<IdentityUser>
    {
        public TasteBudsDbContext() { }
        public TasteBudsDbContext(DbContextOptions<TasteBudsDbContext> options) : base(options) { }
        public DbSet<LoginPage> LoginPages { get; set; }
        public DbSet<RegisterPage> RegisterPages { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<CartModel> carts { get; set; }
        public DbSet<Product> product { get; set; }
    }
}