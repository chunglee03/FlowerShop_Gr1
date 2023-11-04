using System;
using System.Collections.Generic;

namespace adminFlowerShop_Gr1.Models
{
    public partial class TblOrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? OrderNumber { get; set; }
        public int? Quantity { get; set; }
        public int? Discount { get; set; }
        public int? Total { get; set; }
        public DateTime? ShipDate { get; set; }

        public virtual TblOrder? Order { get; set; }
        public virtual TblProduct? Product { get; set; }
    }
}
