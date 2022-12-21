using System.Collections;
using System.Resources;

namespace CreateResource
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateResource();
            Console.WriteLine();
            ReadResource();
        }

        private const string ResourceFile = "Demo.resources";

        static void CreateResource()
        {
            using FileStream fileStream = File.OpenWrite(ResourceFile);
            using ResourceWriter resourceWriter = new ResourceWriter(fileStream);
            resourceWriter.AddResource("Title", "Professional C#");
            resourceWriter.AddResource("Author", "Christian Nagel");
            resourceWriter.AddResource("Publisher", "Wrox Press");
        }

        static void ReadResource()
        {
            using FileStream file = File.OpenRead(ResourceFile);
            using ResourceReader resourceReader = new ResourceReader(file);
            foreach (DictionaryEntry resource in resourceReader)
            {
                Console.WriteLine($"{resource.Key} {resource.Value}");
            }
        }
    }
}