using GitHubApiCall.Helpers;
using GitHubApiCall.Models;
using Newtonsoft.Json;
using Spectre.Console;

namespace GitHubApiCall
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Helper.WelcomeMessage();

            while (true)
            {
                Helper.PrintCommandMessage("Enter command : ");

                var input = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrEmpty(input))
                {
                    Helper.PrintWarningMessage("No input detected, try again !");
                }

                var exit = false;

                switch(input)
                {
                    case "help":
                        HelpMessage();
                        break;
                    case "get-events":
                        PrintUserEvents().Wait();
                        break;
                    case "exit":
                        exit = true;
                        break;
                }

                if (exit)
                {
                    break;
                }
            }
        }

        private static async Task PrintUserEvents()
        {
            var username = Helper.PromptForUsername();
            if (username == null)
                return;

            AnsiConsole.Write(new Rule($"[yellow]GitHub Events for [bold]{username}[/][/]").RuleStyle("grey"));

            var events = await GetUserEventsAsync(username);

            if (events == null || events.Count == 0)
            {
                AnsiConsole.MarkupLine("[italic red]No events to display.[/]");
                return;
            }

            foreach (var element in events)
            {
                var message = element.Type switch
                {
                    "PushEvent" =>
                        $"- Pushed [green]{element.Payload?.Commits?.Count ?? 0}[/] commit(s) to [blue]{element.Repo.Name}[/]",
                    "IssuesEvent" when element.Payload?.Action == "opened" =>
                        $"- Opened a new issue in [blue]{element.Repo.Name}[/]",
                    "WatchEvent" when element.Payload?.Action == "started" => $"- Starred [blue]{element.Repo.Name}[/]",
                    _ => string.Empty
                };
            
                if (!string.IsNullOrEmpty(message))
                    AnsiConsole.MarkupLine(message);
            }
        }

        static async Task<List<GitHubEvent>?> GetUserEventsAsync(string username)
        {
            var url = $"https://api.github.com/users/{username}/events";

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var events = JsonConvert.DeserializeObject<List<GitHubEvent>>(responseBody);

                return events;
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
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
