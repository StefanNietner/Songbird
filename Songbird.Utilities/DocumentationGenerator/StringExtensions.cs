using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentationGenerator
{
    public static class StringExtensions
    {
        public static string SanitizedFilename(this string s)
        {
            return s.Replace('<', '{').Replace('>', '}');
        }
    }
}
