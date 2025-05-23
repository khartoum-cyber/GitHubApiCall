using Spectre.Console;

namespace GitHubApiCall.Helpers
{
    internal static class Helper
    {
        internal static void PrintInfoMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n" + message);
            Console.ResetColor();
        }

        internal static void PrintCommandMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n" + message + "\n");
            Console.ResetColor();
        }

        internal static void PrintWarningMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n" + message);
            Console.ResetColor();
        }

        internal static void WelcomeMessage()
        {
            Helper.PrintInfoMessage("Hello, Welcome to GitHub User Activity!");
            Helper.PrintInfoMessage("Type \"help\" to know the set of API commands.");
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
