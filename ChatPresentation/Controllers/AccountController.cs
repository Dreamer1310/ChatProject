using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatPresentation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(String retrunUrl)
        {
            ViewData["ReturnUrl"] = retrunUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Validate(String username, String password, String returnUrl)
        {
            if (username == "Luka" && password == "123")
            {

                var claims = new List<Claim>();
                claims.Add(new Claim("username", username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                return Redirect("/home/secured");
            }
            return BadRequest();
        }
    }
}
