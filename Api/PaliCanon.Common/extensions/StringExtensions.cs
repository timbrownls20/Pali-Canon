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
            string cleanedText = text;

            //.. remove reference notation i.e. [5]
            Regex referenceNotation = new Regex(@"\[\d+\]");
            cleanedText = referenceNotation.Replace(cleanedText, "");

             //.. remove verse ranges i.e. 271-272.
            Regex verseRange = new Regex(@"\d+-\d+(\.?)");
            cleanedText = verseRange.Replace(cleanedText, "");

            return cleanedText.Trim();
        }

    }

}
