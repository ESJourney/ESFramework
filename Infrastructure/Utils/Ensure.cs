using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utils
{
    public static class Ensure
    {
        public static void NotEmpty(string? text, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException($"The text of '{argumentName}' should not be null or white space.");
        }
        public static void NotNull<T>(T? argument, string argumentName) where T : class
        {
            if (argument == null) throw new ArgumentNullException(argumentName);
        }
    }
}
