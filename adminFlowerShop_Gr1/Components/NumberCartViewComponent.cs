using adminFlowerShop_Gr1.Extension;
using adminFlowerShop_Gr1.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace adminFlowerShop_Gr1.Components
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
           
            return View(cart);
        }
    }
}
