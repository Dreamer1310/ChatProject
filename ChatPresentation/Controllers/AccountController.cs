using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatPresentation.Controllers
{
    [AllowAnonymous]
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

        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("AuthResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public IActionResult FacebookLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("AuthResponse") };
            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> AuthResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var nameIdentifier = result.Principal.Claims.ToList()[0].Value;
            var claims = result.Principal.Identities;


            if (result.Properties.Items[".AuthScheme"] == "Google")
            {

            }
            else if (result.Properties.Items[".AuthScheme"] == "Facebook")
            {

            }


            return RedirectToAction("Secured", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Validate(String username, String password, String returnUrl)
        {
            if (username == "Luka" && password == "123")
            {

                var claims = new List<Claim>();
                claims.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username));
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
