using PInvokeSampleLib;

namespace PInvokeSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine($"usage: {nameof(PInvokeSample)} existingFileName newFileName");
                return;
            }

            var existingFileName = args[0];
            var newFileName = args[1];
            try
            {
                FileUtils.CreateHardLink(existingFileName, newFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}