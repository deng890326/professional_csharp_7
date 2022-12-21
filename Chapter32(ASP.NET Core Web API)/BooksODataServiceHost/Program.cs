using BooksODataService.Models;
using BooksODataService.Services;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BooksODataServiceHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddMvcOptions(option => option.EnableEndpointRouting = false);
            builder.Services.AddOData();
            builder.Services.AddDbContext<BooksContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BooksConnection")));
            builder.Services.AddScoped<CreateBooksService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<CreateBooksService>()
                    .CreateDatabase();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            var odataBuilder = new ODataConventionModelBuilder(app.Services);
            odataBuilder.EntitySet<Book>("Books")
                .EntityType.Count().Page(100, 100).Expand(2);
            odataBuilder.EntitySet<BookChapter>("BookChapters")
                .EntityType.Count().Page(100, 100).Expand(2);

            app.UseMvc(routeBuilder =>
                routeBuilder.MapODataServiceRoute("ODataRoute", "odata",
                    odataBuilder.GetEdmModel()));

            app.Run();
        }
    }
}