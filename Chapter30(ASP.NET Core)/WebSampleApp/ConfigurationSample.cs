namespace WebSampleApp
{
    public class ConfigurationSample
    {
        public ConfigurationSample(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task ShowAppSettingsAsync(HttpContext context)
        {
            string settings = _configuration.GetSection("SampleSettings")["Setting1"];
            await context.Response.WriteAsync(settings.Div());
        }

        public async Task ShowAppSettingsUsingColonsAsync(HttpContext context)
        {
            string settings = _configuration["SampleSettings:Setting1"];
            await context.Response.WriteAsync(settings.Div());
        }

        public async Task ShowConnectionStringAsync(HttpContext context)
        {
            string settings = _configuration.GetConnectionString("DefaultConnection");
            await context.Response.WriteAsync(settings.Div());
        }

        public async Task ShowAppSettingsStronglyTypeAsync(HttpContext context)
        {
            //AppSettings settings = _configuration.GetValue<AppSettings>(nameof(AppSettings));
            AppSettings settings = _configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            await context.Response.WriteAsync(settings.ToString().Div());
        }

        private readonly IConfiguration _configuration;
    }

    public record SubSection1
    {
        public string Setting4 { get; set; }
    }

    public record AppSettings
    {
        public string Setting2 { get; set; }
        public string Setting3 { get; set; }
        public SubSection1 SubSection1 { get; set; }
    }
}
