using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class ProfileController(IUserRepository userRepository) : Controller
    {
        // GET: settings/profile
        [HttpGet("/settings/profile")]
        public async Task<IActionResult> Index()
        {
            var user = await userRepository.GetByUsernameAsync(User.Identity?.Name ?? string.Empty);
            if (user is null)
            {
                return NotFound();
            }

            var model = new ProfileViewModel
            {
                Username = user.Username,
                Email = user.Email,
                Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList()
            };

            return View(model);
        }
    }
}
