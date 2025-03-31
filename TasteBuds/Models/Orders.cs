﻿using System.ComponentModel.DataAnnotations;

namespace TasteBuds.Models
{
    public class Orders
    {
        [Key]
        public int id { get; set; }
        public string? Name { get; set; }
        public int Price {get; set;}
        public int Quantity { get; set; }
    }
}
