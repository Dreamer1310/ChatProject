using ChatPresentation.Models.DB;
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
        private readonly ChatContext _context;

        public AccountController(ChatContext context)
        {
            _context = context;
        }

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

            User user = null;

            if (result.Properties.Items[".AuthScheme"] == "Google")
            {
                user = _context.Users.FirstOrDefault(x => x.GoogleId == nameIdentifier);
                if (user == null)
                {
                    user = new User
                    {
                        GoogleId = nameIdentifier,
                        Email = claims.First().Claims.ToList()[5].Value,
                        Username = claims.First().Claims.ToList()[2].Value,
                        FullName = claims.First().Claims.ToList()[1].Value,
                        Avatar = claims.First().Claims.ToList()[6].Value,
                        RegisteredOn = DateTime.Now
                    };

                    _context.Users.Add(user);
                }
            }
            else if (result.Properties.Items[".AuthScheme"] == "Facebook")
            {
                user = _context.Users.FirstOrDefault(x => x.FacebookId == nameIdentifier);
                if (user == null)
                {
                    user = new User
                    {
                        FacebookId = nameIdentifier,
                        Email = claims.First().Claims.ToList()[1].Value,
                        Username = claims.First().Claims.ToList()[3].Value,
                        FullName = claims.First().Claims.ToList()[2].Value,
                        Avatar = claims.First().Claims.ToList()[5].Value,
                        RegisteredOn = DateTime.Now
                    };

                    _context.Users.Add(user);
                }
            } 

            user.LastVisitedOn = DateTime.Now;
            _context.SaveChanges();

            await HttpContext.SignOutAsync();
            claims.First().RemoveClaim(result.Principal.Claims.ToList()[0]);
            claims.First().AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal);

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
