﻿using System;
using System.Collections.Generic;

namespace adminFlowerShop_Gr1.Models
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            TblPosts = new HashSet<TblPost>();
            TblProducts = new HashSet<TblProduct>();
        }

        public int CatId { get; set; }
        public string? CatName { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public int? Levels { get; set; }
        public int? Ordering { get; set; }
        public bool Published { get; set; }
        public string? Thumb { get; set; }
        public string? Title { get; set; }
        public string? Alias { get; set; }
        public string? MetaDesc { get; set; }
        public string? MetaKey { get; set; }
        public string? Cover { get; set; }
        public string? SchemaMarkup { get; set; }

        public virtual ICollection<TblPost> TblPosts { get; set; }
        public virtual ICollection<TblProduct> TblProducts { get; set; }
    }
}
