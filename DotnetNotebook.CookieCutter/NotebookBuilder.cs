using DotnetNotebook.CookieCutter.Configuration;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace DotnetNotebook.CookieCutter
{
    public static class NotebookBuilder
    {
        private static readonly Regex _variableMatch = new Regex(@"\#!{[^}]*\}");

        public static Root CreateTemplate(ConfigurationInfo info)
        {
            string templateString = File.ReadAllText(info.template);
            var root = JsonConvert.DeserializeObject<Root>(templateString);
            ReplaceVariables(root, info);

            foreach(var templatePath in info.cellsToAddFromTemplate)
            {
                var templateRoot = JsonConvert.DeserializeObject<Root>(File.ReadAllText(templatePath));
                ReplaceVariables(templateRoot, info);
                foreach(var cell in templateRoot.cells)
                {
                    root.cells.Add(cell);
                }
            }

            return root;
        }

        internal static void ReplaceVariables(Root root, ConfigurationInfo info)
        {
            foreach(var cell in root.cells)
            {
                for(int i = 0; i < cell.source.Count; i++)
                {
                    var line = cell.source[i];
                    var match = _variableMatch.Match(line);
                    if (match.Success)
                    {
                        foreach(var group in match.Groups)
                        {
                            var key = group?.ToString().Replace("#!{", "").Replace("}", "") ?? string.Empty;
                            if (!string.IsNullOrWhiteSpace(key) && info.variables.TryGetValue(key, out var value))
                            {
                                if (double.TryParse(value, out var valueAsDouble))
                                {
                                    cell.source[i] = _variableMatch.Replace(line, value);
                                }

                                else
                                {
                                    value = "@\"" + value + "\"";
                                    cell.source[i] = _variableMatch.Replace(line, value); 
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
