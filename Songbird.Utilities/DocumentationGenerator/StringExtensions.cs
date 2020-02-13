using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentationGenerator
{
    public static class StringExtensions
    {
        public static string SanitizedFilename(this string s)
        {
            var retVal = s.Replace('<', '{').Replace('>', '}');
            return retVal;
        }

        public static string AsFormattedMarkdownMethod(this string s)
        {
            return s.Replace("<", @"\<").Replace(",", ", ");
        }
    }
}
