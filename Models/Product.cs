using System;
using System.Collections.Generic;

namespace RedisCache_App.Models
{
    public partial class Product
    {
        public int ProductRowId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryRowId { get; set; }

        public virtual Category CategoryRow { get; set; }
    }
}
