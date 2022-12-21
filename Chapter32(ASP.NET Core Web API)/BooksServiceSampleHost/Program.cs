using BookServices.Services;

namespace BooksServiceSampleHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddSingleton<IBookChaptersService, DefaultBookChaptersService>()
                .AddSingleton<SampleChapters>();
            builder.Services.AddControllers()
                .AddXmlSerializerFormatters();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            using WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Services.GetService<SampleChapters>()?.CreateSampleChapters();

            app.MapControllers();

            app.Run();
        }
    }
}