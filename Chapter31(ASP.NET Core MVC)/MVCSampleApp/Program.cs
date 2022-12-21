using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;
using MVCSampleApp.Services;

namespace MVCSampleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddMvc()
                .AddMvcOptions(option => option.EnableEndpointRouting = false)
                .Services
                .AddScoped<EventsAndMenusContext>()
                .AddSingleton<SampleService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();
            const string baseTemplate = "{controller=Home}/{action=Index}";
            const string defaultTemplate = baseTemplate + "/{id}";
            const string addTemplate = baseTemplate + "/{x=1}/{y=1}";
            const string languageTemplate = "{language}/" + defaultTemplate;
            const string productsTemplate = baseTemplate + "/{productId}";
            app.UseMvc(routes => routes
                .MapRoute("base", baseTemplate)
                .MapRoute("default", defaultTemplate)
                .MapRoute("add", addTemplate)
                .MapRoute("language", languageTemplate, null,
                    constraints: new { language = "(zh)|(en)" })
                .MapRoute("products", productsTemplate, null,
                    constraints: new { productId = @"\d+" }));

            app.Run();
        }
    }
}