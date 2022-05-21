namespace DotnetNotebook.CookieCutter
{
    public enum CellType
    {
        all,
        markdown,
        code
    }

    public static class Consts
    {
        public const string CSharp = "csharp";
        public const string KernelCSharp = "C#";
        public const string LanguageId = "dotnet-interactive.csharp";
    }

    public class Cell
    {
        public string cell_type { get; set; }
        public Metadata metadata { get; set; }
        public List<string> source { get; set; }
        public object execution_count { get; set; }
        public List<object> outputs { get; set; }

        public static Cell CreateMarkdownCell(string contents)
        {
            Cell cell = new Cell()
            {
                cell_type = CellType.markdown.ToString(),
                metadata = new Metadata
                {
                    dotnet_interactive = new DotnetInteractive
                    {
                        language = Consts.CSharp
                    }
                },

                source = contents.Split("\n").ToList(),
            };

            return cell;
        }

        public static Cell CreateCSharpCell(string contents)
        {
            Cell cell = new Cell()
            {
                cell_type = CellType.code.ToString(),
                metadata = new Metadata
                {
                    dotnet_interactive = new DotnetInteractive
                    {
                        language = Consts.CSharp
                    },

                    vscode = new Vscode
                    {
                        languageId = Consts.LanguageId
                    },
                },

                source = contents.Split("\n").ToList(),
            };

            return cell;
        }
    }

    public class DotnetInteractive
    {
        public string language { get; set; }
    }

    public class Kernelspec
    {
        public string display_name { get; set; }
        public string language { get; set; }
        public string name { get; set; }
    }

    public class LanguageInfo
    {
        public string name { get; set; }
    }

    public class Metadata
    {
        public DotnetInteractive dotnet_interactive { get; set; }
        public Vscode vscode { get; set; }
        public Kernelspec kernelspec { get; set; }
        public LanguageInfo language_info { get; set; }

        public static Metadata GetCellDefault()
        {
            Metadata metadata = new Metadata
            {
                dotnet_interactive = new DotnetInteractive
                {
                    language = Consts.CSharp
                }
            };

            return metadata;
        }

        public static Metadata GetNotebookDefault()
        {
            Metadata metadata = new Metadata
            {
                kernelspec = new Kernelspec
                {
                    display_name = ".NET (C#)",
                    language = Consts.KernelCSharp,
                    name = ".net-csharp",
                },
                language_info = new LanguageInfo
                {
                    name = Consts.KernelCSharp
                }
            };

            return metadata;
        }
    }

    public class Root
    {
        public List<Cell> cells { get; set; }
        public Metadata metadata { get; set; }
        public int nbformat { get; set; }
        public int nbformat_minor { get; set; }
    }

    public class Vscode
    {
        public string languageId { get; set; }
    }
}
