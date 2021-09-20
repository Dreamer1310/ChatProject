using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatPresentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services
                .AddDataProtection()
                .SetApplicationName("ChatSenger Rocks!");


            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.Cookie.Name = ".AspNetCore.Demo.ChatSenger";
                    options.Cookie.HttpOnly = false;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.SlidingExpiration = true;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = "995910646393-cmgi11sjdmkv89n09kltut0btej0ih49.apps.googleusercontent.com";
                    options.ClientSecret = "AxhBDapuY5S2IXyz68Lwb3jU";
                    options.Scope.Add("profile");
                    options.Events.OnCreatingTicket = (context) =>
                    {
                        context.Identity.AddClaim(new Claim("picture", context.User.GetProperty("picture").GetString()));

                        return Task.CompletedTask;
                    };
                })
                .AddFacebook(options =>
                {
                    options.AppId = "400727738089956";
                    options.AppSecret = "a57ddcadf631001dca2b2640f0115e01";
                    options.Fields.Add("picture");
                    options.Events.OnCreatingTicket = context =>
                    {
                        context.Identity.AddClaim(new Claim("picture", context.User.GetProperty("picture").GetProperty("data").GetProperty("url").ToString()));
                        //var identity = (ClaimsIdentity)context.Principal.Identity;
                        //var profileImg = context.User["picture"]["data"].Value<string>("url");
                        //identity.AddClaim(new Claim(JwtClaimTypes.Picture, profileImg));
                        return Task.CompletedTask;
                    };
                });

            //services.ConfigureApplicationCookie(opt =>
            //{
            //    opt.LoginPath = "/Account/Login";
            //    opt.Cookie.Name = ".AspNetCore.Demo.Chat";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
