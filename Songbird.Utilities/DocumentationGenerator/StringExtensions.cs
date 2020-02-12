using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentationGenerator
{
    public static class StringExtensions
    {
        public static string SanitizedFilename(this string s, bool isMethod = false)
        {
            var retVal = s.Replace('<', '{').Replace('>', '}');
            if (isMethod && !retVal.Contains('('))
                retVal += "()";
            return retVal;
        }
    }
}
