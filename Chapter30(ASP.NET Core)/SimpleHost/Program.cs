using Microsoft.AspNetCore;

namespace SimpleHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.Start(async context =>
            {
                await context.Response.WriteAsync("<h1> A Simple Host!</h1>");
            }).WaitForShutdown();
        }
    }
}