namespace MVCSampleApp.Services
{
    public class SampleService
    {
        public IEnumerable<string> GetSampleStrings()
        {
            yield return "Deng Yingwei";
            yield return "Chen Lu";
            yield return "My Cats";
        }
    }
}
