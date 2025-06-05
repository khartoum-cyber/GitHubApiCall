using GitHubApiCall.Helpers;
using GitHubApiCall.Interfaces;
using Spectre.Console;

namespace GitHubApiCall
{
    internal class App(IApiCallService apiCallService)
    {
        public void Run()
        {
            Helper.WelcomeMessage();

            while (true)
            {
                Helper.PrintCommandMessage("Enter command : ");
                var input = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrEmpty(input))
                {
                    Helper.PrintWarningMessage("No input detected, try again !");
                    continue;
                }

                switch (input)
                {
                    case "help":
                        Helper.HelpMessage();
                        break;
                    case "get-events":
                        PrintUserEvents().Wait();
                        break;
                    case "get-profile":
                        PrintUserProfile().Wait();
                        break;
                    case "clear":
                        ClearConsole();
                        break;
                    case "exit":
                        return;
                }
            }
        }

        private async Task PrintUserEvents()
        {
            var username = Helper.PromptForUsername();
            if (username == null) return;

            AnsiConsole.MarkupLine("[bold yellow]Filter by event type[/] - enter: [green]PushEvent[/], [green]IssuesEvent[/], [green]WatchEvent[/] or leave empty to list [blue]all events[/].");

            var eventType = Console.ReadLine() ?? string.Empty;

            var rule = new Rule($"GitHub Events for {username}")
                .RuleStyle("yellow")
                .Centered();

            AnsiConsole.Write(rule);

            var events = await apiCallService.GetUserEventsAsync(username);

            if (events == null || events.Count == 0)
            {
                AnsiConsole.MarkupLine("[italic red]No events to display.[/]");
                return;
            }

            Helper.DisplayEvents(events, eventType);
        }

        private async Task PrintUserProfile()
        {
            var username = Helper.PromptForUsername();
            if (username == null) return;

            var rule = new Rule($"GitHub Profile for {username}")
                .RuleStyle("green")
                .Centered();

            AnsiConsole.Write(rule);

            var profile = await apiCallService.GetUserProfileAsync(username);

            if (profile == null)
            {
                AnsiConsole.MarkupLine("[italic red]No profile to display.[/]");
                return;
            }

            Helper.DisplayProfile(profile);
        }

        private void ClearConsole()
        {
            Console.Clear();
        }
    }
}
