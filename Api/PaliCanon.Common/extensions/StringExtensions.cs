using System.IO;
using System.Text.RegularExpressions;

namespace PaliCanon.Common.Extensions 
{

    public static class StringExtensions
    {
            public static string ToApplicationPath(this string fileName)
            {
                var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                return Path.Combine(appRoot, fileName);
            }

            public static string Clean(this string text)
            {
                //.. remove reference notation i.e. [5]
                Regex referenceNotation = new Regex(@"\[\d+\]");
                string cleanedText = referenceNotation.Replace(text, "");
                return cleanedText;
            }

    }

}
