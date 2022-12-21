using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;
using static MVCSampleApp.Controllers.ViewDataKeys;

namespace MVCSampleApp.Controllers
{
    public class SubmitDataController : Controller
    {
        public IActionResult Index()
        {
            ViewData.Model = typeof(SubmitDataController);
            return View();
        }

        public IActionResult CreateMenu1()
        {
            ViewData[CreateMenuResult] = nameof(CreateMenuResult1);
            return View("CreateMenu");
        }

        [HttpPost]
        public IActionResult CreateMenuResult1(int id, string text, double price,
            DateTime date, string category)
        {
            Menu m = new()
            {
                Id = id,
                Text = text,
                Price = price,
                Date = date,
                Category = category
            };
            return View("CreateMenuResult", m);
        }

        public IActionResult CreateMenu2()
        {
            ViewData[CreateMenuResult] = nameof(CreateMenuResult2);
            return View("CreateMenu");
        }

        [HttpPost]
        public IActionResult CreateMenuResult2(Menu menu)
        {
            return View("CreateMenuResult", menu);
        }

        public IActionResult CreateMenu3()
        {
            ViewData[CreateMenuResult] = nameof(CreateMenuResult3);
            return View("CreateMenu");
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuResult3()
        {
            Menu m = new();
            if (await TryUpdateModelAsync(m))
            {
                return View("CreateMenuResult", m);
            }
            return View("Error");
        }

        public IActionResult CreateMenu4()
        {
            ViewData[CreateMenuResult] = nameof(CreateMenuResult4);
            return View("CreateMenu");
        }

        [HttpPost]
        public IActionResult CreateMenuResult4(Menu menu)
        {
            if (ModelState.IsValid)
            {
                return View("CreateMenuResult", menu);
            }
            else
            {
                IEnumerable<KeyValuePair<string, string>> modelStateValues =
                    from s in ModelState
                    select new KeyValuePair<string, string>
                        (s.Key, string.Join(", ", s.Value.GetPropertyValues()));
                return View("CreateMenuResult", new Menu()
                {
                    Text = string.Join(";    ", modelStateValues)
                });
            };
        }

    }
}