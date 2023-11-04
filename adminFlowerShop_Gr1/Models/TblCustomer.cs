using System;
using System.Collections.Generic;

namespace adminFlowerShop_Gr1.Models
{
    public partial class TblCustomer
    {
        public TblCustomer()
        {
            TblOrders = new HashSet<TblOrder>();
        }

        public int CustomerId { get; set; }
        public string? FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? LocationId { get; set; }
        public int? District { get; set; }
        public int? Ward { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool? Active { get; set; }

        public virtual TblLocation? Location { get; set; }
        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
