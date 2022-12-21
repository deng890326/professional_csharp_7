using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MVCSampleApp.Models;
using System.Diagnostics;
using System.Text.Encodings.Web;

namespace MVCSampleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string Hello() => $"Hello, ASP.NET Core MVC Test!!!";

        public string Greeting(string name = "default") =>
            $"Hello, {name}";

        public string Greeting2(string id = "default") =>
            $"Hello, {id}";

        public string Add(int x = 5, int y = 1) =>
            $"{x} + {y} = {x + y}";

        public IActionResult ContentDemo() =>
            Content("Hello, ContentDemo", "text/plain");

        public IActionResult JsonDemo() =>
            Json(new
            {
                Id = 3,
                Text = "Coffee with milk",
                Price = 5.99m.ToString("C"),
                Date = DateTime.Now.ToString("G"),
                Category = "Drinks"
            });

        public IActionResult RedirectDemo() =>
            Redirect("Add/3/5");

        public IActionResult RedirectDemo2() =>
            Redirect("https://www.google.com");

        public IActionResult RedirectDemo3() =>
            Redirect("../Demo");
        public IActionResult RedirectDemo4() =>
            Redirect("../Demo/Index");

        public IActionResult RedirectDemo5a() =>
            Redirect($"{nameof(RedirectDemo5)}/100");

        public IActionResult RedirectDemo5(int id = 1) =>
            Redirect($"{++id}");

        public IActionResult RedirectDemo6() =>
            Redirect("/Demo/Index");

        public IActionResult FileDemo()
        {
            string Pictures = Environment.GetFolderPath(
                Environment.SpecialFolder.MyPictures);
            FileStream stream = new(Pictures +
                $"{Path.DirectorySeparatorChar}image.jpg", FileMode.Open);

            Task.Run(() =>
            {
                Thread.Sleep(1000);
                try
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    int b = stream.ReadByte();
                    _logger.LogError($"stream is still open, b={b}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "error reading stream");
                }
            });

            return File(stream, "image/jpeg");
        }


        public IActionResult Index()
        {
            ViewData.Model = typeof(HomeController);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}