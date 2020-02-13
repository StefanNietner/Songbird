using Songbird.Utilities.Guards;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentationGenerator.Models
{
    public class Method
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public List<TypeParameter> TypeParameters { get; set; }
        public List<Parameter> Parameters { get; set; }
        public List<PossibleException> Exceptions { get; set; }
        public string Example { get; set; }
        public string Returns { get; set; }
        public string Remarks { get; set; }

        public Method()
        {
            TypeParameters = new List<TypeParameter>();
            Parameters = new List<Parameter>();
            Exceptions = new List<PossibleException>();
        }
        public void ReplaceTypePlaceholders()
        {
            if (Name.Contains("``"))
            {
                Guard.IsInteger(Name[(Name.IndexOf("``") + 2)..(Name.IndexOf("("))], out int typeCount);
                var typeString = "<" + string.Join(",", TypeParameters.Select(p => p.Name)) + ">";
                Name = Name.Replace($"``{typeCount}", typeString);
                for (int i = 0; i < typeCount; i++)
                {
                    Name = Name.Replace($"``{i}", TypeParameters[i].Name);
                }
            }
        }
    }
}