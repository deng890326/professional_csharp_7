using Microsoft.AspNetCore.Mvc;

namespace MVCSampleApp.Controllers
{
    public class DemoController : Controller
    {
        public string Index() =>
            HttpContext.Request.Path;
    }
}
