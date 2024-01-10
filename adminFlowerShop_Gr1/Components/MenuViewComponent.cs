using adminFlowerShop_Gr1.Models;
using Microsoft.AspNetCore.Mvc;

namespace adminFlowerShop_Gr1.Components
{
    [ViewComponent(Name ="MenuView")]
    public class MenuViewComponent: ViewComponent
    {
        private FlowerShop_Group1Context _context;
        public MenuViewComponent(FlowerShop_Group1Context context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listMenu = (from m in _context.Menus
                            where (m.IsActive == true) && (m.Position == 1)
                            select m).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listMenu));
        }
    }
}
