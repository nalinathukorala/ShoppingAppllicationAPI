using System;
using System.Collections.Generic;

namespace OnlineShopping.DataAccess.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderItems = new HashSet<OrderItems>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }

        public virtual Categories Category { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
