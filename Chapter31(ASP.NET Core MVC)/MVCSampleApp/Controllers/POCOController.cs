using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static MVCSampleApp.Controllers.ViewDataKeys;

namespace MVCSampleApp.Controllers
{
    public class POCOController
    {
        public IActionResult Index() =>
            new ViewResult()
            {
                ViewData = new ViewDataDictionary<Type>(
                    new EmptyModelMetadataProvider(), ModelState)
                {
                    [Title] = nameof(Index),
                    Model = typeof(POCOController)
                }
            };

        [ActionContext]
        public ActionContext ActionContext { get; set; }

        public HttpContext HttpContext => ActionContext.HttpContext;

        public ModelStateDictionary ModelState => ActionContext.ModelState;

        public string UserAgentInfo() =>
            (string?)HttpContext.Request.Headers["User-Agent"]
            ?? "No User-Agent";

        public async Task FileDemo()
        {
            string Pictures = Environment.GetFolderPath(
                Environment.SpecialFolder.MyPictures);
            using FileStream stream = new(Pictures +
                $"{Path.DirectorySeparatorChar}image.jpg", FileMode.Open);

            await new FileStreamResult(stream, "image/jpeg")
                .ExecuteResultAsync(ActionContext);
        }
    }
}
