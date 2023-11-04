using System;
using System.Collections.Generic;

namespace adminFlowerShop_Gr1.Models
{
    public partial class TblAttributesPrice
    {
        public int AttributesPriceId { get; set; }
        public int? AttributeId { get; set; }
        public int? ProductId { get; set; }
        public int? Price { get; set; }
        public bool? Active { get; set; }

        public virtual TblAttribute? Attribute { get; set; }
        public virtual TblProduct? Product { get; set; }
    }
}
