using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PInvokeSampleLib
{
    public static class FileUtils
    {
        public static void CreateHardLink(string oldFileName, string NewFileName) =>
            NativeMethods.CreateHardLink(NewFileName, oldFileName);
    }
}
