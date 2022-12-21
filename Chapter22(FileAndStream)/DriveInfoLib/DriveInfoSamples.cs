using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace DriveInfoLib
{
    public static class DriveInfoSamples
    {
        public static string[] GetDriveInfoTexts()
        {
            DriveInfo[] driveInfos = DriveInfo.GetDrives();
            string[] result = new string[driveInfos.Length + 1];
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Drive Summary:");
            sb.TryAppendLine("Total Drives: {0}", () => driveInfos.Length);
            for (int i = 0; i < driveInfos.Length; i++)
            {
                sb.TryAppendLine($"Drive[{i}]: {{0}}", () => driveInfos[i].Name);
            }
            result[0] = sb.ToString();

            for (int i = 0; i < driveInfos.Length; i++)
            {
                sb.Clear();
                DriveInfo driveInfo = driveInfos[i];
                sb.TryAppendLine("Drive name: {0}", () => driveInfo.Name);
                sb.TryAppendLine("Drive Format: {0}", () => driveInfo.DriveFormat);
                sb.TryAppendLine("Dirve Type: {0}", () => driveInfo.DriveType);
                sb.TryAppendLine("Dirve Root: {0}", () => driveInfo.RootDirectory);
                sb.TryAppendLine("Volume Label: {0}", () => driveInfo.VolumeLabel);
                sb.TryAppendLine("Free Space: {0}", () => driveInfo.TotalFreeSpace);
                sb.TryAppendLine("Available: Space: {0}", () => driveInfo.AvailableFreeSpace);
                sb.TryAppendLine("Total Size: {0}", () => driveInfo.TotalSize);
                sb.TryAppendLine("Is Ready: {0}", () => driveInfo.IsReady);
                result[i + 1] = sb.ToString();
            }

            return result;


        }

        private static StringBuilder TryAppendLine(this StringBuilder sb, string format, Func<object?> propertyFunc)
        {
            try
            {
                sb.AppendLine(string.Format(format, propertyFunc()));
            }
            catch (Exception ex)
            {
                sb.AppendLine(string.Format(format, ex.ToString()));
            }

            return sb;
        }

        public static string[] GetSpecialFolderInfoTexts()
        {
            Array specialFolders = Enum.GetValues(typeof(Environment.SpecialFolder));
            string[] result = new string[specialFolders.Length];
            StringBuilder sb = new StringBuilder();
            //IEnumerator< Environment.SpecialFolder> specialFolderEnum =
            //    (IEnumerator<Environment.SpecialFolder>)specialFolders.GetEnumerator();
            for (int i = 0; i < result.Length; i++)
            {
                //specialFolderEnum.MoveNext();
                Environment.SpecialFolder value =
                    (Environment.SpecialFolder)specialFolders.GetValue(i)!;
                sb.Clear();
                string name = Enum.GetName(value) ?? "null";
                sb.AppendLine($"SpecialFolder {name} Info:");
                string path = Environment.GetFolderPath(value);
                sb.AppendLine($"Path: {path}");
                if (path.Length > 0 && Directory.Exists(path))
                {
                    sb.AppendLine($"Sub Directiories:");
                    IEnumerable<string> dirs = Directory.EnumerateDirectories(path).DefaultIfEmpty("no directories");
                    sb.AppendLine($"\t{string.Join("\n\t", dirs)}");
                    sb.AppendLine($"Contains Files:");
                    IEnumerable<string> files = Directory.EnumerateFiles(path).DefaultIfEmpty("no files");
                    sb.AppendLine($"\t{string.Join("\n\t", files)}");
                }
                result[i] = sb.ToString();
            }
            return result;
        }
    }
}