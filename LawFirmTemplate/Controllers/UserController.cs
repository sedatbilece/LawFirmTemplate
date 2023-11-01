using LawFirmTemplate.Data;
using LawFirmTemplate.Data.Entities;
using LawFirmTemplate.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LawFirmTemplate.Controllers
{
    public class UserController : Controller
    {

        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {    
            return View( new UserIndexViewModel() );
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserIndexViewModel model)
        {
            var user = _context.Users.FirstOrDefault(x=>x.Password == model.Password && x.UserName==model.UserName);

            if (user != null && user.RoleType==Data.Enums.RoleType.Admin)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,model.UserName)
                };

                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24), // Set the expiration time of the cookie (24 hours from now)
                    IsPersistent = true // Set the cookie as persistent
                };

                await HttpContext.SignInAsync(principal, authProperties);

                return RedirectToAction("Index", "Admin");
            }

                return View(new UserIndexViewModel());
        }


        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }


    }
}
