using System;
using System.Collections.Generic;

namespace OnlineShopping.DataAccess.Models
{
    public partial class Orders
    {
        public Orders()
        {
            EmailStatus = new HashSet<EmailStatus>();
            OrderItems = new HashSet<OrderItems>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public byte OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public byte PaymentMethod { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<EmailStatus> EmailStatus { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
