
using adminFlowerShop_Gr1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using adminFlowerShop_Gr1.Helpper;

namespace adminFlowerShop_Gr1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly FlowerShop_Group1Context _context;
        public LoginController(FlowerShop_Group1Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(AdminUser user)
        {
            if (user == null)
            {
                return NotFound();
            }
            string pw = Utilities.MD5Password(user.Password);

            var check = _context.TblAccounts.Where(m => (m.Email == user.Email) && (m.Password == pw)).FirstOrDefault();
            if (check == null)
            {
                Utilities._Message = "Invalid UserName or Password";
                return RedirectToAction("Index", "Login");
            }
            Utilities._Message = string.Empty;
            Utilities._UserID = check.AccountId;
            Utilities._UserName = string.IsNullOrEmpty(check.FullName) ? string.Empty : check.FullName;
            Utilities._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;

            return RedirectToAction("Index", "Home");
        }
    }
}
