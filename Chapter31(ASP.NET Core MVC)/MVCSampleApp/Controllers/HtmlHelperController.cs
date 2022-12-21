using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;

namespace MVCSampleApp.Controllers
{
    public class HtmlHelperController : Controller
    {
        public IActionResult Index()
        {
            return View(typeof(HtmlHelperController));
        }

        public IActionResult SimpleHelper()
        {
            return View();
        }

        public IActionResult HelperWithMenu() =>
            View(GetSampleMenu());

        public IActionResult HelperList() =>
            View(GetSampleList());

        public IActionResult StronglyTypeMenu() =>
            View(GetSampleMenu());

        public IActionResult EditorExtensions() =>
            View(GetSampleMenu());

        [HttpPost]
        public IActionResult EditorResult(Menu menu)
        {
            return View(nameof(StronglyTypeMenu), menu);
        }

        private static IDictionary<int, string> GetSampleList() =>
            new Dictionary<int, string>()
            {
                [1] = "Red Bull Racing",
                [2] = "McLaren",
                [3] = "Mercedes",
                [4] = "Ferrari"
            };

        private static Menu GetSampleMenu() =>
            new()
            {
                Id = 1,
                Text = "Coffee with milk",
                Price = 9.99,
                Date = DateTime.Now,
                Category = "Drinks"
            };
    }
}
