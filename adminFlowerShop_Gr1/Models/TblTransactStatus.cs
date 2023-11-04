using System;
using System.Collections.Generic;

namespace adminFlowerShop_Gr1.Models
{
    public partial class TblTransactStatus
    {
        public TblTransactStatus()
        {
            TblOrders = new HashSet<TblOrder>();
        }

        public int TransactStatusId { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
