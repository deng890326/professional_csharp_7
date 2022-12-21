namespace WebSampleApp.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderMiddleware(this IApplicationBuilder app) => app.UseMiddleware<HeaderMiddleware>();
    }
}
