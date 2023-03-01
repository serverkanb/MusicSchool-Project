using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MusicSchool.Areas.Account.Controllers
{
    [Area("Account")]
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;

        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AccountRegisterModel accountRegisterModel)
        {
            if (ModelState.IsValid)
            {
                var result = _accountService.Register(accountRegisterModel);
                if (result.IsSuccessful)
                {
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", result.Message);

            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountLoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel userResult = new UserModel();
                var Result=_accountService.Login(model,userResult);
                if (Result.IsSuccessful)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,userResult.UserName),
                        new Claim(ClaimTypes.Role,userResult.RoleName),
                        new Claim(ClaimTypes.Sid, userResult.Id.ToString())
                    };
                    var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    var princapal=new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princapal);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                ModelState.AddModelError("", Result.Message);
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
