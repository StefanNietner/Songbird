using Microsoft.Extensions.Configuration;
using System;
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
                Console.WriteLine(filePath);
                var doc = XDocument.Load(filePath);
                var members = doc.Element("doc").Element("members");
                var reg = new Regex(@"[.]\S+[(]");
                foreach (var type in members.Elements().Where(xel=>xel.Attribute("name").Value.StartsWith("T:")))
                {
                    Console.WriteLine($"Type found: {type.Attribute("name").Value.Substring(2)}");
                    Console.WriteLine("Contains the following Properties:");
                    var properties = members.Elements().Where(xel =>
                        xel.Attribute("name").Value.StartsWith($"P:{type.Attribute("name").Value.Substring(2)}")
                        && !reg.Match(xel.Attribute("name").Value.Substring(type.Attribute("name").Value.Length + 1)).Success);
                    foreach (var property in properties)
                    {
                        //Console.WriteLine(method.Attribute("name").Value);
                        Console.WriteLine(property.Attribute("name").Value.Substring(type.Attribute("name").Value.Length + 1));
                    }
                    Console.WriteLine("Contains the following Methods:");
                    var methods = members.Elements().Where(xel => 
                        xel.Attribute("name").Value.StartsWith($"M:{type.Attribute("name").Value.Substring(2)}") 
                        && !reg.Match(xel.Attribute("name").Value.Substring(type.Attribute("name").Value.Length + 1)).Success);
                    foreach (var method in methods)
                    {
                        //Console.WriteLine(method.Attribute("name").Value);
                        Console.WriteLine(method.Attribute("name").Value.Substring(type.Attribute("name").Value.Length + 1));
                    }
                }
            }
            else
            {
                Console.WriteLine($"No Documentation file found for {configSection.Key}");
            }
        }
    }
}
