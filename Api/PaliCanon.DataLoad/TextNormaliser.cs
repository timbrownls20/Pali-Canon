using System.Collections.Generic;

namespace PaliCanon.DataLoad
{
    internal static class TextNormaliser
    {
        internal static string RemoveHtmlEntities(string text)
        {
            List<(string find, string replacement)> scans = new List<(string regex, string replacement)>();
            scans.Add(("&mdash;", " - "));

            foreach((string find, string replacement) in scans)
            {
                text = text.Replace(find, replacement);
            }
            return text;
        }
    }
}
