using DotnetNotebook.CookieCutter.Configuration;

using YamlDotNet.Serialization.NamingConventions;

namespace DotnetNotebook.CookieCutter
{
    internal class ConfigurationParser
    {
        public static ConfigurationInfo ParseConfiguration(string path)
        {
            var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
                                             .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                             .Build();
            return deserializer.Deserialize<ConfigurationInfo>(File.ReadAllText(path));
        }
    }
}
