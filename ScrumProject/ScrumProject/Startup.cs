using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using ScrumProject.Models.DataLayer;

namespace ScrumProject
{
    public class Startup
    {
        public IConfiguration ConfigRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            ConfigRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<ScrumProjectContext>(options => options.UseSqlServer(ConfigRoot.GetConnectionString("SPContext")));
            
            // Prevents Cross-Site Request Forgery (CSRF) attacks through web forms.
            services.AddAntiforgery();
            

            // Removes server version information. 
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AddServerHeader = false;
            });
            //Setting up user role for AuthUser
            services.AddIdentity<AuthUser, IdentityRole>()
                .AddEntityFrameworkStores<ScrumProjectContext>()
                .AddDefaultTokenProviders();
            //added sessionstate
            services.AddSession();
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

                app.UseHsts();
            }
            // Remove exposure of sensitive information.
            app.Use(async (context, next) =>
            {
                // No Framing
                context.Response.Headers["X-Frame-Options"] = "DENY";
                // Avoid attacks based on MIME-type confusion, telling browser to adhere to MIME-types registered.
                context.Response.Headers["X-Content-Type-Options"] = new StringValues("nosniff");
                context.Response.Headers.Remove("X-Aspnet-version");
                context.Response.Headers.Remove("X-AspNetMvc-Version");
                context.Response.Headers.Remove("Server");
                context.Response.Headers.Remove("x-powered-by");
                await next();
            });
            // Prevents user from using untrusted/invalid certificates.
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();
            //added sessionstate
            app.UseSession();
            //changed default routes to add admin area
            /*
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            */
#pragma warning disable ASP0014 // Suggest using top level route registrations
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
#pragma warning restore ASP0014 // Suggest using top level route registrations
            app.Run();
        }
    }
}