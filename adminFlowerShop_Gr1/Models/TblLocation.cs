using System;
using System.Collections.Generic;

namespace adminFlowerShop_Gr1.Models
{
    public partial class TblLocation
    {
        public TblLocation()
        {
            TblCustomers = new HashSet<TblCustomer>();
        }

        public int LocationId { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Slug { get; set; }
        public string? NameWithType { get; set; }
        public string? PathWithType { get; set; }
        public int? ParentCode { get; set; }
        public int? Levels { get; set; }

        public virtual ICollection<TblCustomer> TblCustomers { get; set; }
    }
}
