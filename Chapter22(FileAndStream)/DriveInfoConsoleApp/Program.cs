using DriveInfoLib;

namespace DriveInfoConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //foreach (string driveInfo in DriveInfoSamples.GetDriveInfoTexts())
            //{
            //    Console.WriteLine(driveInfo);
            //}

            foreach (string info in DriveInfoSamples.GetSpecialFolderInfoTexts())
            {
                Console.WriteLine(info);
            }
        }
    }
}