using GitHubApiCall.Helpers;
using GitHubApiCall.Interfaces;
using GitHubApiCall.Models;
using GitHubApiCall.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Spectre.Console;

namespace GitHubApiCall
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().AddSingleton<IApiCallService, ApiCallService>().BuildServiceProvider();
            var apiCallService = serviceProvider.GetService<IApiCallService>();

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
                        PrintUserEvents();
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

            void PrintUserEvents()
            {
                var username = Helper.PromptForUsername();
                if (username == null)
                    return;

                AnsiConsole.Write(new Rule($"[yellow]GitHub Events for [bold]{username}[/][/]").RuleStyle("grey"));

                var events =  apiCallService?.GetUserEventsAsync(username).Result;

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
