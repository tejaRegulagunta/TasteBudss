// File: Models/CartModel.cs
using System.ComponentModel.DataAnnotations;

namespace TasteBuds.Models
{
    public class CartModel
    {
        [Key]
        public int id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }
}
