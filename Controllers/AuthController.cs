using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class AuthController(IUserRepository userRepository) : Controller
    {
        // GET: /login
        [HttpGet]
        [Route("/login")]
        public ActionResult Index()
        {
            return View();
        }

        // POST: /login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/login")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), model);
            }
            var user = await userRepository.GetByUsernameAsync(model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(nameof(Index), model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
            claims.AddRange(user.UserRoles.Select(ur => new Claim(ClaimTypes.Role, ur.Role.Code)));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        // POST: /logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }

        // GET: /access-denied
        [HttpGet]
        [Route("/access-denied")]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
