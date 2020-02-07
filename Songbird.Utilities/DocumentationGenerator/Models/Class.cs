using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentationGenerator.Models
{
    public class Class
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public List<Property> Properties { get; set; }
        public List<Method> Methods { get; set; }

        public Class()
        {
            Properties = new List<Property>();
            Methods = new List<Method>();
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Type found: {Name}");
            sb.AppendLine($"{Summary}");
            if (Properties.Any())
            {
                sb.AppendLine("Contains the following Properties:");
                foreach (var item in Properties)
                {
                    sb.Append(item.ToString());
                }
            }
            if (Methods.Any())
            {
                sb.AppendLine("Contains the following Methods:");
                foreach (var item in Methods)
                {
                    sb.AppendLine(item.ToString());
                }
            }
            sb.AppendLine();
            sb.AppendLine("----");
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
