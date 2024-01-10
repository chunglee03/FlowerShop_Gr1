using adminFlowerShop_Gr1.Models;
using Microsoft.AspNetCore.Mvc;

namespace adminFlowerShop_Gr1.Components
{
    [ViewComponent(Name = "Post")]
    public class PostComponent : ViewComponent
    {
        private readonly FlowerShop_Group1Context _context;
        public PostComponent(FlowerShop_Group1Context context)
        {
            _context = context;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listPost = (from p in _context.TblPosts
                            where (p.Published == true)
                            orderby p.PostId descending
                            select p).Take(3).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listPost));
        }
    }
}
