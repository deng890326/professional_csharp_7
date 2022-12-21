using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;

namespace MVCSampleApp.Controllers
{
    public class TagHelpersController : Controller
    {
        public IActionResult Index()
        {
            return View(typeof(TagHelpersController));
        }

        public IActionResult Sample()
        {
            return View();
        }

        public IActionResult LabelHelper() =>
            View(GetSampleMenu());

        public IActionResult InputHelper() =>
            View(GetSampleMenu());

        public IActionResult FormHelper(Menu? m = null)
        {
            switch (HttpContext.Request.Method)
            {
                case "POST":
                    {
                        if (ModelState.IsValid)
                        {
                            return RedirectToAction(nameof(FormHelperResult), m);
                        }
                        else
                        {
                            return View(m ?? GetSampleMenu());
                        }
                    }
                case "GET":
                    return View(m ?? GetSampleMenu());
                default:
                    return View("Error");
            }
        }

        public IActionResult FormHelperResult(Menu menu)
        {
            return View(menu);
        }

        public IActionResult EnvironmentHelper()
        {
            return View();
        }

        public IActionResult Markdown() => View();

        public IActionResult CustomTable() => View(GetSampleMenus());

        private static IEnumerable<Menu> GetSampleMenus()
        {
            yield return new ()
            {
                Id = 1,
                Text = "Coffee",
                Price = 9.9,
                Date = DateTime.Now,
                Category = "Drinks"
            };
            yield return new()
            {
                Id = 2,
                Text = "Tea",
                Price = 5.9,
                Date = DateTime.Now,
                Category = "Drinks"
            };
            yield return new()
            {
                Id = 3,
                Text = "Bread",
                Price = 2.9,
                Date = DateTime.Now,
                Category = "Main"
            };
        }

        private static Menu GetSampleMenu() =>
            new()
            {
                Id = 1,
                Text = "Coffee",
                Price = 9.9,
                Date = DateTime.Now,
                Category = "Drinks"
            };
    }
}
