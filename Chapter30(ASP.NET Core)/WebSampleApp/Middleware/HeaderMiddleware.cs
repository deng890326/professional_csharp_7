namespace WebSampleApp.Middleware
{
    public class HeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public HeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context) => Task.Run(() =>
        {
            context.Response.Headers.Add("SampleHeader", new[] { "HeaderFromMiddleware" });
            //Thread.Sleep(TimeSpan.FromSeconds(2));

            return _next.Invoke(context);
        });
    }
}
