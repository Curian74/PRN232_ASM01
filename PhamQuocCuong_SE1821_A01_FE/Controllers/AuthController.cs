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
        private readonly SystemAccountService _systemAccountService;

        public AuthController(AuthService authService, SystemAccountService systemAccountService)
        {
            _authService = authService;
            _systemAccountService = systemAccountService;
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

        public async Task<IActionResult> Profile(short id)
        {
            var account = await _systemAccountService.GetByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            var modelType = new EditAccountDto
            {
                AccountEmail = account.AccountEmail,
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountPassword = account.AccountPassword,
                AccountRole = account.AccountRole,
            };

            return View(modelType);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(EditAccountDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var result = await _systemAccountService.UpdateAsync(model);

                TempData["success"] = "Update successfully!";

                return View(model);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return View(model);
            }
        }
    }
}
