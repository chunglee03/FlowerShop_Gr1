            using adminFlowerShop_Gr1.Extension;
using adminFlowerShop_Gr1.Helpper;
using adminFlowerShop_Gr1.Models;
using adminFlowerShop_Gr1.ModelViews;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace adminFlowerShop_Gr1.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly FlowerShop_Group1Context _context;
        public INotyfService _notyfService { get; }
        public AccountsController(FlowerShop_Group1Context context,INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidatePhone( string Phone)
        {
            try
            {
                var khachhang = _context.TblCustomers.AsNoTracking().SingleOrDefault(x => x.Phone.ToLower() == Phone.ToLower());
                if (khachhang != null)
                    return Json(data: "Số điện thoại:" + Phone + " đã được sử dụng");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidateEmail(string Email)
        {
            try
            {
                var khachhang = _context.TblCustomers.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
                if (khachhang != null)
                    return Json(data: "Email:" + Email + " đã được sử dụng");
                return Json(data: true);

            }
            catch
            {
                return Json(data: true);
            }
        }

        
        [HttpGet]
        [AllowAnonymous]
        [Route("dangky.html", Name = "dangky")]
        public IActionResult DangKiTaiKhoan()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("dangky.html", Name = "dangky")]
        public async Task<IActionResult> DangKiTaiKhoan(RegisterVM taikhoan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string salt = Utilities.GetRandomKey();
                    TblCustomer khachhang = new TblCustomer
                    {
                        FullName = taikhoan.FullName,
                        Phone = taikhoan.Phone.Trim().ToLower(),
                        Email = taikhoan.Email.Trim().ToLower(),
                        Password = (taikhoan.Password + salt.Trim()).ToMD5(),
                        Active = true,
                        Salt = salt,
                        CreateDate = DateTime.Now
                    };
                    try
                    {
                        _context.Add(khachhang);
                        await _context.SaveChangesAsync();

                        HttpContext.Session.SetString("CustomerId", khachhang.CustomerId.ToString());
                        var taikhoanID = HttpContext.Session.GetString("CustomerID");

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, khachhang.FullName),
                            new Claim("CustomerID", khachhang.CustomerId.ToString())
                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        _notyfService.Success("login success");
                        return RedirectToAction("Dashboard", "Accounts");
                    }
                    catch
                    {
                        return RedirectToAction("DangKiTaiKhoan", "Accounts");
                    }
                }
                else
                {
                    return View(taikhoan);
                }
            }
            catch
            {
                return View(taikhoan);
            }
        }
		
		[AllowAnonymous]
        [Route("dangnhap.html", Name = "dangnhap")]
        public IActionResult Login(string? returnUrl = null)
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerID");
            if (taikhoanID != null)
            {
                
                return RedirectToAction("Dashboard", "Accounts");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("dangnhap.html", Name = "dangnhap")]
        public async Task<IActionResult> Login(LoginViewModel customer, string? returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isEmail = Utilities.IsValidEmail(customer.UserName);
                    if (!isEmail) return View(customer);
                    var khachhang = _context.TblCustomers.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == customer.UserName);

                    if (khachhang == null) return RedirectToAction("DangKiTaiKhoan");

                    string pass = (customer.Password + khachhang.Salt.Trim()).ToMD5();
                    if (khachhang.Password != pass)
                    {
                        _notyfService.Success("Sai thông tin đăng nhập");
                        return View(customer);
                    }
                    if (khachhang.Active == false) return RedirectToAction("Thông báo", "Accounts");

                    HttpContext.Session.SetString("CustomerID", khachhang.CustomerId.ToString());
                    var taikhoanID = HttpContext.Session.GetString("CustomerID");

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, khachhang.FullName),
                        new Claim("CustomerID",khachhang.CustomerId.ToString())
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    _notyfService.Success("Đăng nhập thành công");
                    return RedirectToAction("Dashboard", "Accounts");
                }

            }
            catch
            {
                return RedirectToAction("DangKiTaiKhoan", "Accounts");

            }
            return View(customer);

        }
		[Route("taikhoancuatoi.html", Name = "Dashboard")]
		public IActionResult Dashboard()
		{
			var taikhoanID = HttpContext.Session.GetString("CustomerID");
			if (taikhoanID != null)
			{
				var khachhang = _context.TblCustomers.AsNoTracking().SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taikhoanID));
				if (khachhang != null)
				{
					var IsDonhang = _context.TblOrders
						.Include(x => x.TransactStatus)
						.AsNoTracking()
						.Where(x => x.CustomerId == khachhang.CustomerId)
						.OrderByDescending(x => x.OrderDate).ToList();
					ViewBag.Donhang = IsDonhang;
					return View(khachhang);
				}
			}
			return RedirectToAction("Login");
		}
		[HttpGet]
        [Route("dangxuat.html",Name ="Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("CustomerID");
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var taikhoanID = HttpContext.Session.GetString("CustomerID");
                if (taikhoanID == null)
                {
                    return RedirectToAction("Login", "Accounts");

                }
                if(ModelState.IsValid)
                {
                    var taikhoan = _context.TblCustomers.Find(Convert.ToInt32(taikhoanID));
                    if (taikhoan == null) return RedirectToAction("Login", "Accounts");
                    var pass = (model.PasswordNow.Trim() + taikhoan.Salt.Trim()).ToMD5();
                    if (pass == taikhoan.Password)
                    {
                        string passnew = (model.Password.Trim() + taikhoan.Salt.Trim()).ToMD5();
                        taikhoan.Password = passnew;
                        _context.Update(taikhoan);
                        _context.SaveChanges();
                        _notyfService.Success(" thanh cong");
                        return RedirectToAction("Dashboard", "Accounts");
                    }
                }
                
            }
            catch
            {
                _notyfService.Success("Đổi mật khẩu không thành công");
                return RedirectToAction("Dashboard", "Accounts");
            }
            _notyfService.Success("Đổi mật khẩu không thành công");
            return RedirectToAction("Dashboard", "Accounts");
        }
    }
}
