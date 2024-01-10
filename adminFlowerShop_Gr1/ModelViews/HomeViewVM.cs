using adminFlowerShop_Gr1.Controllers;
using adminFlowerShop_Gr1.Models;

namespace adminFlowerShop_Gr1.ModelViews
{
    public class HomeViewVM
    {
        public List<ProductHomeVM> ?Products { get; set; }
        public List<TblPost>? Blog { get; set; }
    }
}
    