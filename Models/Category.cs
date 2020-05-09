using System;
using System.Collections.Generic;

namespace RedisCache_App.Models
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public int CategoryRowId { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BasePrice { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
