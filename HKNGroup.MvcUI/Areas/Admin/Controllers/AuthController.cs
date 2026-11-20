using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HKNGroup.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly IAdminService _adminService;

        public AuthController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View("LogIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginDto adminLoginDto, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var authResult = await _adminService.Authenticate(adminLoginDto.UserName, adminLoginDto.Password);

                if (authResult.IsSuccess && authResult.Data?.Admin != null)
                {
                    await SignInAdminAsync(authResult.Data.Admin.UserName);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, authResult.Message ?? "Geçersiz kullanıcı adı veya şifre.");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View("LogIn", adminLoginDto);
        }

        private async Task SignInAdminAsync(string username)
        {
            var claims = new List<Claim>
            {
                new Claim("Username", username),
                new Claim(ClaimTypes.Name, username)
            };

            var identity = new ClaimsIdentity(claims, "login");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string sqAnswer)
        {
            return View(sqAnswer);
        }
    }
}
