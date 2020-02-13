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
        public List<Method> Constructors { get; set; }

        public Class()
        {
            Properties = new List<Property>();
            Methods = new List<Method>();
        }
    }
}
