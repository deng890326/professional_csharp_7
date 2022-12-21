namespace WebSampleApp.Services
{
    public class DefaultSampleService : ISampleService
    {
        public IEnumerable<string> GetSampleStrings()
        {
            return new[] { "one", "two", "three" };
        }
    }
}
