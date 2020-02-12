using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace DocumentationGenerator
{
    public static class XElementExtensions
    {
        public static bool IsPropertyOf(this XElement element, XElement type)
        {
            return IsXOf(element, type, "P");
        }
        public static bool IsMethodOf(this XElement element, XElement type)
        {
            return IsXOf(element, type, "M");
        }
        private static bool IsXOf(XElement element, XElement type, string typeIndicator)
        {
            var match = true;
            var reg = new Regex(@"[.]\S+[(]");
            match &= element.Attribute("name").Value.StartsWith($"{typeIndicator}:{type.Attribute("name").Value.Substring(2)}.")
                  && !reg.Match(element.Attribute("name").Value.Substring(type.Attribute("name").Value.Length + 1)).Success;
            return match;
        }

        public static string MethodOrPropertyName(this XElement element, XElement type)
        {
            return element.Attribute("name").Value.Substring(type.Attribute("name").Value.Length + 1);
        }

        public static string FullValue(this XElement element)
        {
            static string formatted(string val) => 
                string.Join("  \r\n", val.Split("\r\n")
                .Select(sub => sub.StartsWith(new string(' ', 12)) ? sub.Substring(12) : sub));
            static string Codeformatted(string val) => string.Join("  \n", val.Split("\n")
                                                                                .Where(sub => !string.IsNullOrWhiteSpace(sub))
                                                                                .Select(sub => sub.Substring(12)));
            var s = "";
            foreach (var node in element.Nodes())
            {
                if(node.NodeType == System.Xml.XmlNodeType.Element)
                {
                    if (node is XElement n)
                    {
                        if (n.Name == "paramref")
                            s += $"`{n.Attribute("name").Value}`";
                        else if (n.Name == "see")
                            s += $"`{n.Attribute("cref").Value}`";
                        else if (n.Name == "typeparamref")
                            s += $"`{n.Attribute("name").Value}`";
                        else if (n.Name == "code")
                            s += "```c#\r\n" + Codeformatted(n.Value) + "\r\n```";
                    }
                }
                else if (node.NodeType == System.Xml.XmlNodeType.Text)
                {
                    s += node;
                }
            }
            return formatted(s);
        }
    }
}
