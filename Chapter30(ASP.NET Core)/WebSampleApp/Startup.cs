using System.Text;
using WebSampleApp.Controllers;
using WebSampleApp.Middleware;
using WebSampleApp.Services;

namespace WebSampleApp
{
    internal class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISampleService, DefaultSampleService>()
                .AddTransient<HomeController>()
                .AddDistributedMemoryCache()
                .AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(10))
                .AddTransient<ConfigurationSample>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles()
                .UseSession()
                .UseHeaderMiddleware();

            const string Home = $"/{nameof(Home)}";
            const string Config = $"/{nameof(Config)}";
            const string Session = $"/{nameof(Session)}";
            const string RequestAndResponse = $"/{nameof(RequestAndResponse)}";

            string[] topPath =
            {
                Home, Config, Session, RequestAndResponse
            };

            app.Map(Config, app => app.Run(
                context =>
                {
                    const string appsettings = $"/{nameof(appsettings)}";
                    const string colons = $"/{nameof(colons)}";
                    const string database = $"/{nameof(database)}";
                    const string stronglytyped = $"/{nameof(stronglytyped)}";
                    ConfigurationSample configurationSample =
                        app.ApplicationServices.GetRequiredService<ConfigurationSample>();
                    var value = context.Request.Path.Value;
                    return value switch
                    {
                        appsettings =>
                            configurationSample.ShowAppSettingsAsync(context),
                        colons =>
                            configurationSample.ShowAppSettingsUsingColonsAsync(context),
                        database =>
                            configurationSample.ShowConnectionStringAsync(context),
                        stronglytyped =>
                            configurationSample.ShowAppSettingsStronglyTypeAsync(context),
                        string path when isDefaultPath(path) =>
                            showDefaultAsync(context, new string[] {
                                appsettings, colons, database, stronglytyped }),
                        _ =>
                            context.Response.WriteAsync($"Path {value} is not supported.".Div())
                    };


                }));

            app.Map(Session, app => app.Run(
                context =>
                {
                    return SessionSample.SessionAsync(context);
                }));

            app.Map(Home, app => app.Run(
                context =>
                {
                    IServiceProvider services = app.ApplicationServices;
                    HomeController controller = services.GetRequiredService<HomeController>();
                    return controller.Index(context);
                }));

            app.Map(RequestAndResponse, app => app.Run(
                context =>
                {
                    context.Response.ContentType = "text/html";
                    const string header = $"/{nameof(header)}";
                    const string add = $"/{nameof(add)}";
                    const string content = $"/{nameof(content)}";
                    const string encoded = $"/{nameof(encoded)}";
                    const string form = $"/{nameof(form)}";
                    const string writecookie = $"/{nameof(writecookie)}";
                    const string readcookie = $"/{nameof(readcookie)}";
                    const string json = $"/{nameof(json)}";
                    const string request = $"/{nameof(request)}";
                    var value = context.Request.Path.Value;
                    string result = value switch
                    {
                        header =>
                            context.Request.GetHeaderInfo(),
                        add =>
                            context.Request.QueryString(),
                        content =>
                            context.Request.GetContent(),
                        encoded =>
                            context.Request.GetContentEncoded(),
                        form =>
                            context.Request.GetForm(),
                        writecookie =>
                            context.Response.WriteCookie(),
                        readcookie =>
                            context.Request.ReadCookie(),
                        json =>
                            context.Response.GetJson(),
                        request =>
                            context.Request.GetInfo(),
                        string path when isDefaultPath(path) =>
                            "",
                        _ =>
                            $"Path {value} is not supported.".Div(),
                    };
                    if (result.Length > 0)
                    {
                        return context.Response.WriteAsync(result);
                    }
                    else
                    {
                        return showDefaultAsync(context, new string[] {
                            header, add, content, encoded, form, writecookie,
                            readcookie, json, request});
                    }
                    
                }));

            app.MapWhen(context => context.Request.Path.Value is string path && path.ToUpper().Contains("hello"),
                        app => app.Run(context => context.Response.WriteAsync("hello in the path".Div())));

            app.MapWhen(context => context.Request.Path.Value is string path && isDefaultPath(path),
                        app => app.Run(context => showDefaultAsync(context, topPath)));

            app.Run(context => context.Response.WriteAsync($"Path {context.Request.Path} is not supported.".Div()));

            static bool isDefaultPath(string path) => path == "" || path == "/";

            static async Task showDefaultAsync(HttpContext context, string[] paths)
            {
                StringBuilder retBuilder = new();
                retBuilder.Append("<ul>");
                foreach (string path in paths)
                {
                    retBuilder.Append(path.Li());
                }
                retBuilder.Append("</ul>");
                await context.Response.WriteAsync("Available Path".Div(retBuilder.ToString()));
            }
        }


    }
}