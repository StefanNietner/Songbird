using DocumentationGenerator.Models;
using Microsoft.Extensions.Configuration;
using Songbird.Utilities.Guards;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace DocumentationGenerator
{
    class Program
    {
#pragma warning disable IDE0060
        static void Main(string[] args)
#pragma warning restore IDE0060

        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            var projectPaths = configuration.GetSection("Projects").GetChildren();

            foreach (var item in projectPaths)
            {
                Console.WriteLine($"Beginning Process for {item.Key}");
                ExtractDocumentation(item);
            }

            Console.ReadLine();
        }

        private static void ExtractDocumentation(IConfigurationSection configSection)
        {
            string binPath;
#if DEBUG
            binPath = @"bin\Debug\netcoreapp3.1";
#else
            binPath = @"bin\Release\netcoreapp3.1";
#endif
            var filePath = Path.Combine(configSection.Value, binPath, $"{configSection.Key}.xml");
            if (File.Exists(filePath))
            {
                var classes = new List<Class>();
                Console.WriteLine(filePath);
                var doc = XDocument.Load(filePath);
                var members = doc.Element("doc").Element("members");
                var types = members.Elements().Where(xel => xel.Attribute("name").Value.StartsWith("T:"));
                foreach (var type in types)
                {
                    var currentClass = new Class()
                    {
                        Name = type.Attribute("name").Value.Substring(2),
                        Summary = type.Element("summary").FullValue(),
                        Properties = members.Elements().Where(xel => xel.IsPropertyOf(type)).Select(p =>
                            new Property()
                            {
                                Name = p.MethodOrPropertyName(type),
                                Summary = p.Element("summary").FullValue(),
                            }).ToList(),
                        Methods = members.Elements().Where(xel => xel.IsMethodOf(type)).Select(m =>
                        new Method()
                        {
                            Name = m.MethodOrPropertyName(type),
                            Summary = m.Element("summary").FullValue(),
                            TypeParameters = m.Elements("typeparam").Select(t =>
                                new TypeParameter()
                                {
                                    Name = t.Attribute("name").Value,
                                    Summary = t.FullValue(),
                                }).ToList(),
                            Parameters = m.Elements("param").Select(p =>
                                new Parameter()
                                {
                                    Name = p.Attribute("name").Value,
                                    Summary = p.FullValue(),
                                }).ToList(),
                            Exceptions = m.Elements("exception").Select(ex =>
                                new PossibleException()
                                {
                                    CRef = ex.Attribute("cref").Value,
                                    Summary = ex.FullValue(),
                                }).ToList(),
                            Example = m.Element("example")?.FullValue() ?? ""
                        }).ToList(),
                    };
                    classes.Add(currentClass);
                    Console.WriteLine(currentClass);
                }
            }
            else
            {
                Console.WriteLine($"No Documentation file found for {configSection.Key}");
            }
        }
    }
}
