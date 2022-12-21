using System.Text;
using WebSampleApp.Services;

namespace WebSampleApp.Controllers
{
    public class HomeController
    {
        public HomeController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public Task Index(HttpContext context) => Task.Run(() =>
        {
            
            StringBuilder retBuilder = new();
            retBuilder.Append("<ul>");
            foreach (string str in _sampleService.GetSampleStrings())
            {
                retBuilder.Append(str.Li());
            }
            retBuilder.Append("</ul>");
            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/html";
            //Thread.Sleep(TimeSpan.FromSeconds(2));

            return context.Response.WriteAsync(retBuilder.ToString());
        });


        private readonly ISampleService _sampleService;
    }
}
