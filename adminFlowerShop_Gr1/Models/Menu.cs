using System;
using System.Collections.Generic;

namespace adminFlowerShop_Gr1.Models
{
    public partial class Menu
    {
        public int MenuId { get; set; }
        public string? MenuName { get; set; }
        public bool? IsActive { get; set; }
        public int? Levels { get; set; }
        public int? ParentId { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string? Link { get; set; }
        public int? MenuOrder { get; set; }
        public int? Position { get; set; }
    }
}
