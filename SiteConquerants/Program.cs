using SiteConquerants.Models;
using Microsoft.Extensions.Configuration;
using SiteConquerants.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace SiteConquerants
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(300);
                options.Cookie.Name = ".ConquerantsLimoilou.Session";
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddScoped<YoutubeAPI>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.MapRazorPages();

            var configuration = app.Services.GetRequiredService<IConfiguration>();

            //les routes
            app.MapControllerRoute(name: "confidentialite",
                pattern: "confidentialite",
                defaults: new { controller = "Home", action = "Confidentialite" });
            app.MapControllerRoute(name: "redirectionvideo",
                pattern: "/r/{id}",
                defaults: new { controller = "Home", action = "RedirectionVideo" });
            app.MapControllerRoute(name: "historique",
                pattern: "historique",
                defaults: new { controller = "Home", action = "Historique" });
            app.MapControllerRoute(name: "index",
                pattern: "/",
                defaults: new { controller = "Home", action = "Index" });
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}