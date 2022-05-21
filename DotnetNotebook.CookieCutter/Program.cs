using CommandLine;
using DotnetNotebook.CookieCutter.Configuration;
using Newtonsoft.Json;

namespace DotnetNotebook.CookieCutter
{
    public class Program 
    {
        public class Options
        {
            [Option(
                shortName: 'c', 
                longName: "configPath", 
                Default = "./Config.yaml",
                HelpText = "Configuration File with pertinent details such as variables and templates to consider.")]
            public string ConfigurationPath { get; set; }

            [Option(
                shortName: 'o',
                longName: "outputPath", 
                Default = "./Converted.ipynb",
                HelpText = "Output path where the notebook should be created to." )]
            public string OutputPath { get; set; }
        }

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    ConfigurationInfo info = ConfigurationParser.ParseConfiguration(o.ConfigurationPath);
                    var root = NotebookBuilder.CreateTemplate(info);
                    string serialized = JsonConvert.SerializeObject(root, Formatting.Indented);
                    File.WriteAllText(o.OutputPath, serialized);
                });
        }
    }
}
