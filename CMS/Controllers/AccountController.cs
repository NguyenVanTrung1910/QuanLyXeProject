using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Infrastructure.Persitence.SqlServer;
using Domain.Querys.Base;
using Domain.IRepositories.SqlServer;
using System.Text;

namespace CMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INguoiDungRepository _nguoiDungRepository;
        public AccountController(INguoiDungRepository nguoiDungRepository, IHttpContextAccessor httpContextAccessor)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _httpContextAccessor = httpContextAccessor;

        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Kiểm tra tên đăng nhập và mật khẩu
            //password = "***************";
            var user = _nguoiDungRepository.CheckNguoiDungDangNhap(username, password);
            if (user.Id != 0)
            {
                string key = $"User:{user.Id};#{user.TenDangNhap}";
                //HttpContext.Session.SetString("redisKeySession", key);
                string jsonUserData = JsonConvert.SerializeObject(user);
                _httpContextAccessor?.HttpContext?.Session.SetString("dataUser", jsonUserData);
                //HttpContext.Session.SetString("TenNguoiDung", user.TenNguoiDung ?? "");
                //HttpContext.Session.SetInt32("Id", user.Id);
                //HttpContext.Session.SetInt32("CurrentCongTyOfUserId", user.CongTyId ?? 0);
                //HttpContext.Session.SetInt32("CurrentRoleOfUserId", user.VaiTro ?? 0);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.TenNguoiDung ?? ""),
                    new Claim(ClaimTypes.Email, user.Email ?? ""),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Đăng nhập người dùng và tạo cookie
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Index", "NguoiDung");  // Sau khi đăng nhập thành công, chuyển hướng đến trang chủ
            }

            // Nếu thông tin đăng nhập không đúng, hiển thị thông báo lỗi
            ViewBag.Error = "Thông tin đăng nhập không đúng";
            return View("Login","Account");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");  // Sau khi đăng xuất, chuyển hướng đến trang chủ
        }
        public ActionResult ErrorPage()
        {
            return View();
        }

    }
}
