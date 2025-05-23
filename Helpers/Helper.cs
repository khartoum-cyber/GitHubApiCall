using Spectre.Console;

namespace GitHubApiCall.Helpers
{
    internal static class Helper
    {
        internal static void PrintInfoMessage(string message)
        {
            AnsiConsole.MarkupLine($"\n[blue]{EscapeMarkup(message)}[/]");
        }

        internal static void PrintCommandMessage(string message)
        {
            AnsiConsole.MarkupLine($"\n[bold white]{EscapeMarkup(message)}[/]\n");
        }

        internal static void PrintWarningMessage(string message)
        {
            AnsiConsole.MarkupLine($"\n[red]{EscapeMarkup(message)}[/]");
        }

        // Optional: Escape markup characters like [ and ] to avoid formatting issues
        private static string EscapeMarkup(string input)
        {
            return input.Replace("[", "[[").Replace("]", "]]");
        }

        internal static void WelcomeMessage()
        {
            // Create a dynamic ASCII banner using FigletText
            var banner = new FigletText("GitHubApiCall")
                .Centered()
                .Color(Color.Green);

            AnsiConsole.Write(banner);

            var panel = new Panel(
                new Markup("[bold yellow]Hello, Welcome to GitHub User Activity![/]\n[green]Type [bold]\"help\"[/] to know the set of API commands.[/]").Centered()
            )
            {
                Border = BoxBorder.Rounded,
                Padding = new Padding(1, 1),
                Header = new PanelHeader("[blue]Welcome[/]", Justify.Center)
            };

            AnsiConsole.Write(panel);
        }

        internal static string? PromptForUsername(int maxRetries = 5)
        {
            var retryCount = 0;

            while (retryCount < maxRetries)
            {
                var username = AnsiConsole.Prompt(
                    new TextPrompt<string>("[green]Enter GitHub username:[/]")
                        .PromptStyle("cyan")
                        .AllowEmpty());


                if (!string.IsNullOrWhiteSpace(username))
                    return username.Trim();

                retryCount++;
                AnsiConsole.MarkupLine("[red]Username cannot be empty.[/] Press [yellow]Esc[/] to exit or try again.");

                var keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    AnsiConsole.MarkupLine("\n[red]ESC key pressed. Exiting...[/]");
                    return null;

                }
            }

            AnsiConsole.MarkupLine("[red]Too many failed attempts. Exiting...[/]");
            return null;
        }
    }
}
