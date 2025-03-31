using System.ComponentModel.DataAnnotations;

namespace TasteBuds.Models
{
    public class FoodItems
    {
        [Key]
        public int ItemId { get; set;}

        public string? ItemName { get; set; }

        public int Price { get; set; }

    }
}
