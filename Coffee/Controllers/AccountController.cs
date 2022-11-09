using Coffee.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography;
using Coffee.Interfaces;

namespace Coffee.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICrudlUserService _crudlUserService;

        public AccountController(ICrudlUserService crudlUserService)
        {
            _crudlUserService = crudlUserService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _crudlUserService.GetUserByLogin(model.Login);
                var dbpwd = user.Password;
                var mpwd = model.Password;
                if (IRegistrationService.VerifyHashedPassword(dbpwd, mpwd))
                {

                    if (user != null)
                    {
                        await Authenticate(model.Login); // аутентификация

                        Response.Cookies.Append("userId", $"{user.Id}");

                        if (user.RoleId == 1)
                        {
                            return Redirect($"~/Home/AdminFirstPage");
                        }
                        else
                        {
                            return Redirect($"~/Home/UserFirstPage");

                        }
                    }
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _crudlUserService.GetUserByLogin(model.Login);
                if (user == null)
                {
                    await _crudlUserService.CreateUser(model.Name, model.Login, model.Password, model.RoleId);

                    await Authenticate(model.Login); // аутентификация

                    return RedirectToAction("Login");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
