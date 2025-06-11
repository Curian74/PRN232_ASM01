using DataAccessObjects.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PhamQuocCuong_SE1821_A01_FE.Services;
using System.Security.Claims;

namespace PhamQuocCuong_SE1821_A01_FE.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            try
            {
                var user = await _authService.Login(loginDto);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                    return View(loginDto);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.AccountId.ToString()),
                    new Claim(ClaimTypes.Email, user.AccountEmail!.ToString()),
                    new Claim(ClaimTypes.Name, user.AccountName!.ToString()),
                    new Claim(ClaimTypes.Role, user.AccountRole!.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);
                
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync("CookieAuth", principal, authProperties);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(loginDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
