using adminFlowerShop_Gr1.Helpper;
using adminFlowerShop_Gr1.Models;
using Microsoft.AspNetCore.Mvc;

namespace adminFlowerShop_Gr1.Areas.Admin.Controllers
{
    [Area("Admin")]


    public class RegisterController : Controller
    {
        private readonly FlowerShop_Group1Context _context;
        public RegisterController(FlowerShop_Group1Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(TblAccount user)
        {
            if (user == null)
            {
                return NotFound();
            }

            var check = _context.TblAccounts.Where(m => m.Email == user.Email).FirstOrDefault();
            if (check != null)
            {
                Utilities._MessageEmail = "Duplicate Email!";
                return RedirectToAction("Index", "Register");
            }
            Utilities._MessageEmail = string.Empty;
            user.Password = Utilities.MD5Password(user.Password);
            _context.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "Login");
        }
    }
}