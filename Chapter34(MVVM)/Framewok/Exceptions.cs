using System;

namespace Framewok
{
    public static class Exceptions
    {
        public static ArgumentNullException Null(string paramName) =>
            new ArgumentNullException(paramName);
    }
}
