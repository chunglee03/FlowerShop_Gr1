using adminFlowerShop_Gr1.Models;
using adminFlowerShop_Gr1.ModelViews;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using adminFlowerShop_Gr1.Extension;

namespace adminFlowerShop_Gr1.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly FlowerShop_Group1Context _context;
        public INotyfService _notyfService { get; }
        public ShoppingCartController(FlowerShop_Group1Context context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        public List<CartItem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;
            }
        }


        [HttpPost]
        [Route("api/cart/add")]
        public IActionResult AddToCart(int? productID,int? amount)
        {
            List<CartItem> giohang = GioHang;
            try
            {
                //them sp
                CartItem? item = giohang.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null) //sam pham da co => tang so luong
                {
                    if (amount.HasValue)
                    {
                        item.amount = amount.Value;
                    }
                    else
                    {
                        item.amount++;
                    }
                }
                else
                {
                    TblProduct hh = _context.TblProducts.SingleOrDefault(p => p.ProductId == productID);
                    item = new CartItem
                    {
                        amount = amount.HasValue ? amount.Value : 1,
                        product = hh
                    };
					giohang.Add(item);// them vao gio hang
                }
                HttpContext.Session.Set<List<CartItem>>("GioHang", giohang);
                _notyfService.Success("Đã thêm sản phẩm vào giỏ hàng");
                return Json(new { success = true });
            }
            catch(Exception ex)
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [Route("api/cart/update")]
        public IActionResult UpdateCart(int productId,int? amount)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            try
            {
                if (cart != null)
                {
                    CartItem item = cart.SingleOrDefault(p => p.product.ProductId == productId);
                    if(item != null && amount.HasValue)
                    {
                        item.amount = amount.Value;
                    }
                    HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                }
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [Route("api/cart/remove")]
        public ActionResult Remove(int? productID)
        {
            try
            {
                List<CartItem> giohang = GioHang;
                CartItem item = giohang.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null)
                {
                    giohang.Remove(item);
                }
                HttpContext.Session.Set<List<CartItem>>("Giohang", giohang);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [Route("cart.html", Name = "Cart")]
        public IActionResult Index()
        {
            return View(GioHang);
        }
    }
}
