using MenuPlanner.Models;
using MenuPlanner.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MenuPlanner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MenuCardsContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddScoped<IMenuCardsService, MenuCardsService>();
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            //builder.Services.AddIdentity<ApplicationUser, IndentityRole<string>>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();
            //builder.Services.AddTransient<IEmailSender, EmailSender>();



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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}