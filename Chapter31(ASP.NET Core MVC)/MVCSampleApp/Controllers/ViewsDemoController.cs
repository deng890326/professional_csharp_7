using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MVCSampleApp.Models;
using static MVCSampleApp.Controllers.ViewDataKeys;

namespace MVCSampleApp.Controllers
{
    public class ViewsDemoController : Controller
    {
        public ViewsDemoController(ILogger<ViewsDemoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData.Model = typeof(ViewsDemoController);
            return View();
        }

        public IActionResult PassingViewData()
        {
            ViewData[MyData] = "Hello";
            return new ViewResult
            {
                ViewData = ViewData
            };
        }

        public IActionResult PassingAModel()
        {
            return View(GetSampleData());
        }

        public IActionResult LayoutUsingSections()
        {
            return View();
        }

        public IActionResult UseAPartialView1([FromServices] EventsAndMenusContext context)
        {
            return View(model: context);
        }

        public IActionResult UseAPartialView2([FromServices] EventsAndMenusContext context)
        {
            return View(model: context);
        }

        public IActionResult ShowEvents([FromServices] EventsAndMenusContext context)
        {
            ViewData[EventTitle] = "Live Events";
            return PartialView(model: context.Events);
        }

        public IActionResult UseViewModelComponent1()
        {
            return View();
        }

        public IActionResult UseViewModelComponent2()
        {
            return View();
        }

        public IActionResult InjectServiceInView()
        {
            return View();
        }

        private static IEnumerable<string> GetSampleData() =>
            new string[]
            {
                "Coffee",
                "Red Tea",
                "Green Tea",
                "Milk"
            };

        [ActionContext]
        public ActionContext ActionContext { get; set; }

        public HttpContext HttpContext => ActionContext.HttpContext;

        public ModelStateDictionary ModelState => ActionContext.ModelState;

        private ILogger<ViewsDemoController> _logger;
    }
}
