using BookServices.Models;
using BookServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BookFunctionsApp
{
    public static class BookFunction
    {
        static BookFunction()
        {
            ConfigureServices();
            FeedSampleChapters();
            GetRequiredServices();
        }

        private static void FeedSampleChapters()
        {
            var sampleChapters = ApplicationServices.GetRequiredService<SampleChapters>();
            sampleChapters.CreateSampleChapters();
        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IBookChaptersService, DefaultBookChaptersService>();
            services.AddSingleton<SampleChapters>();
            ApplicationServices = services.BuildServiceProvider();
        }

        private static void GetRequiredServices()
        {
            s_bookChaptersService =
              ApplicationServices.GetRequiredService<IBookChaptersService>();
        }

        private static IBookChaptersService s_bookChaptersService;


        public static IServiceProvider ApplicationServices { get; private set; }

        [FunctionName("BookFunction")]
        public static Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            Task<IActionResult> result = null;
            switch (req.Method)
            {
                case "GET":
                    result = DoGetAsync(req);
                    break;
                case "POST":
                    result = DoPostAsync(req);
                    break;
                case "PUT":
                    result = DoPutAsync(req);
                    break;
                default:
                    break;
            }

            return result;
        }

        private static async Task<IActionResult> DoGetAsync(HttpRequest req)
        {
            var bookChapterService = ApplicationServices.GetRequiredService<IBookChaptersService>();
            var chapters = await bookChapterService.GetAll();
            return new OkObjectResult(chapters);
        }

        private static async Task<IActionResult> DoPostAsync(HttpRequest req)
        {
            string json = await new StreamReader(req.Body).ReadToEndAsync();
            BookChapter chapter = JsonConvert.DeserializeObject<BookChapter>(json);
            await s_bookChaptersService.Add(chapter);
            return new OkResult();
        }


        private static async Task<IActionResult> DoPutAsync(HttpRequest req)
        {
            string json = await new StreamReader(req.Body).ReadToEndAsync();
            BookChapter chapter = JsonConvert.DeserializeObject<BookChapter>(json);
            await s_bookChaptersService.Update(chapter);
            return new OkResult();
        }
    }
}
