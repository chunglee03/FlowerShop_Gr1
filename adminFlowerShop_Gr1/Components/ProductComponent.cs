using adminFlowerShop_Gr1.Models;
using Microsoft.AspNetCore.Mvc;

namespace adminFlowerShop_Gr1.Components
{
    [ViewComponent(Name ="Product")]
    public class ProductComponent: ViewComponent
    {
        private readonly FlowerShop_Group1Context _context;
        public ProductComponent(FlowerShop_Group1Context context)
        {
            _context = context;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listProduct=(from p in _context.TblProducts
                             where (p.Active==true)orderby p.ProductId descending
                             select p).Take(8).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listProduct));
        }
       
    }
}
