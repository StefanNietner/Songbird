using DocumentationGenerator.Models;
using Microsoft.Extensions.Configuration;
using Songbird.Utilities.Guards;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace DocumentationGenerator
{
    class Program
    {
        static string _documentationPath;
        static IEnumerable<IConfigurationSection> _projectPaths;
#pragma warning disable IDE0060
        static void Main(string[] args)
#pragma warning restore IDE0060

        {
            ReadConfig();

            foreach (var item in _projectPaths)
            {
                Console.WriteLine($"Beginning Process for {item.Key}");
                ExtractDocumentation(item);
            }

            Console.ReadLine();
        }

        private static void ReadConfig()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            _projectPaths = configuration.GetSection("Projects").GetChildren();

            _documentationPath = configuration.GetSection("DocumentationPath").Value;
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
                    var currentClass = ExtractClass(members, type);
                    classes.Add(currentClass);
                    Console.WriteLine(currentClass);
                }
                foreach (var item in classes)
                {
                    WriteMarkdownDocumentation(item);
                }
            }
            else
            {
                Console.WriteLine($"No Documentation file found for {configSection.Key}");
            }

        }

        private static Class ExtractClass(XElement members, XElement type)
        {
            return new Class()
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
                                        Example = m.Element("example")?.FullValue() ?? "",
                                        Returns = m.Element("returns")?.FullValue() ?? "",
                                        Remarks = m.Element("remarks")?.FullValue() ?? "",
                                    }).ToList(),
            };
        }

        private static void WriteMarkdownDocumentation(Class item)
        {
            if (!Directory.Exists(_documentationPath)) return;
            var classDir = Path.Combine(_documentationPath, item.Name);
            Directory.CreateDirectory(classDir);
            var sb = new StringBuilder();
            sb.AppendLine($"# {item.Name}");
            sb.AppendLine(item.Summary.Trim());

            AddPropertySection(item, sb);
            AddMethodsAndCreateFile(item, classDir, sb);
            File.WriteAllText(Path.Combine(classDir, $"{item.Name.SanitizedFilename()}.md"), sb.ToString());
        }

        private static void AddMethodsAndCreateFile(Class item, string classDir, StringBuilder sb)
        {
            if (item.Methods.Any())
            {
                Directory.CreateDirectory(Path.Combine(classDir, "Methods"));
                sb.AppendLine($"## Methods");
                sb.AppendLine("|Name|Summary|");
                sb.AppendLine("|-|-|");
                foreach (var method in item.Methods)
                {
                    CreateMethodDocumentationFile(classDir, method);
                    sb.AppendLine($"[{method.Name.Replace("<", @"\<")}](Methods/{method.Name.SanitizedFilename()})|{method.Summary.Trim()}");
                }
            }
        }

        private static void AddPropertySection(Class item, StringBuilder sb)
        {
            if (item.Properties.Any())
            {
                sb.AppendLine($"## Properties");
                sb.AppendLine("|Name|Summary|");
                sb.AppendLine("|-|-|");
                foreach (var property in item.Properties)
                {
                    sb.AppendLine($"{property.Name}|{property.Summary.Trim()}");
                }
            }
        }

        private static void CreateMethodDocumentationFile(string classDir, Method method)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"# {method.Name.Replace("<", @"\<")}");
            sb.AppendLine($"{method.Summary.Trim()}");

            if (!string.IsNullOrEmpty(method.Remarks))
            {
                sb.AppendLine($"## Remarks");
                sb.AppendLine($"{method.Remarks}");
            }

            if (method.TypeParameters.Any())
            {
                sb.AppendLine($"## Type parameters");
                sb.AppendLine("|Name|Summary|");
                sb.AppendLine("|-|-|");
                foreach (var item in method.TypeParameters)
                {
                    sb.AppendLine($"|{item.Name}|{item.Summary.Trim()}|");
                }
            }

            if (method.Parameters.Any())
            {
                sb.AppendLine($"## Parameters");
                sb.AppendLine("|Name|Summary|");
                sb.AppendLine("|-|-|");
                foreach (var item in method.Parameters)
                {
                    sb.AppendLine($"|{item.Name}|{item.Summary.Trim()}|");
                }
            }

            if (!string.IsNullOrEmpty(method.Returns))
            {
                sb.AppendLine($"## Returns");
                sb.AppendLine($"{method.Returns}");
            }

            if (method.Exceptions.Any())
            {
                sb.AppendLine($"## Exceptions");
                foreach (var item in method.Exceptions)
                {
                    sb.AppendLine($"{item.CRef.Substring(2)}  ");
                    sb.AppendLine($"{item.Summary}");
                    sb.AppendLine();
                }
            }

            if (!string.IsNullOrEmpty(method.Example))
            {
                sb.AppendLine($"## Example");
                sb.AppendLine($"{method.Example}");
            }

            File.WriteAllText(Path.Combine(classDir, "Methods", $"{method.Name.SanitizedFilename()}.md"), sb.ToString());
        }
    }
}
