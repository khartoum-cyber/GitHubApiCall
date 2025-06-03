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
                        HelpMessage();
                        break;
                    case "get-events":
                        PrintUserEvents().Wait();
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

            foreach (var element in events)
            {
                var message = element.Type switch
                {
                    "PushEvent" => $"- Pushed [green]{element.Payload?.Commits?.Count ?? 0}[/] commit(s) to [blue]{element.Repo.Name}[/]",
                    "IssuesEvent" when element.Payload?.Action == "opened" => $"- Opened a new issue in [blue]{element.Repo.Name}[/]",
                    "WatchEvent" when element.Payload?.Action == "started" => $"- Starred [blue]{element.Repo.Name}[/]",
                    _ => string.Empty
                };

                if (!string.IsNullOrEmpty(message))
                    AnsiConsole.MarkupLine(message);
            }
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
        }

        static void HelpMessage()
        {
            var count = 1;
            var list = new List<string>
            {
                "get-events - To list events for given user.",
                "exit - To close the app",
                "clear - To clear console window"
            };

            foreach (var element in list)
            {
                Console.WriteLine($"{count} : {element}");
                count++;
            }
        }
    }
}
