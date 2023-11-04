using System;
using System.Collections.Generic;

namespace adminFlowerShop_Gr1.Models
{
    public partial class TblAttribute
    {
        public TblAttribute()
        {
            TblAttributesPrices = new HashSet<TblAttributesPrice>();
        }

        public int AttributeId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<TblAttributesPrice> TblAttributesPrices { get; set; }
    }
}
