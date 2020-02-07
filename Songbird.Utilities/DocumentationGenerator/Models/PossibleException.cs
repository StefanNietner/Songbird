using System.Text;

namespace DocumentationGenerator.Models
{
    public class PossibleException
    {
        public string CRef { get; set; }
        public string Summary { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(CRef);
            sb.AppendLine(Summary);
            sb.AppendLine();
            return sb.ToString();
        }
    }
}