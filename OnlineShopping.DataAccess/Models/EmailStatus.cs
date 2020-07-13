using System;
using System.Collections.Generic;

namespace OnlineShopping.DataAccess.Models
{
    public partial class EmailStatus
    {
        public int MailId { get; set; }
        public int? OrderId { get; set; }
        public byte EmailStatus1 { get; set; }

        public virtual Orders Order { get; set; }
    }
}
