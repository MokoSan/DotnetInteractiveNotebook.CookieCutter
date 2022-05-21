namespace DotnetNotebook.CookieCutter.Configuration
{
    public sealed class ConfigurationInfo
    {
        public Dictionary<string, string> variables { get; set; }
        public string template { get; set; }
        public List<string> cellsToAddFromTemplate { get; set; }
    }
}
