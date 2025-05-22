using GitHubApiCall.Helpers;
using GitHubApiCall.Models;
using Newtonsoft.Json;

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
                        GetUserEventsAsync().Wait();
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

        static async Task GetUserEventsAsync()
        {
            string? username = string.Empty;
            int retryCount = 0;
            const int maxRetries = 5;

            while (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("\nEnter GitHub username:");
                username = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(username))
                {
                    retryCount++;
                    Console.WriteLine("Username cannot be empty. Please try again or press Esc to exit.");

                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("\nESC key pressed. Exiting...");
                        return;
                    }

                    if (retryCount >= maxRetries)
                    {
                        Console.WriteLine("Too many failed attempts. Exiting...");
                        return;
                    }
                }
            }

            var url = $"https://api.github.com/users/{username}/events";

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var events = JsonConvert.DeserializeObject<List<GitHubEvent>>(responseBody);

                if (events == null || events.Count == 0)
                    Console.WriteLine("No events or username not found.");

                foreach (var element in events)
                {
                    switch (element.Type)
                    {

                        case "PushEvent":
                            int commitCount = element.Payload?.Commits?.Count ?? 0;
                            Console.WriteLine($"- Pushed {commitCount} commit(s) to {element.Repo.Name}");
                            break;

                        case "IssuesEvent":
                            if (element.Payload?.Action == "opened")
                                Console.WriteLine($"- Opened a new issue in {element.Repo.Name}");
                            break;

                        case "WatchEvent":
                            if (element.Payload?.Action == "started")
                                Console.WriteLine($"- Starred {element.Repo.Name}");
                            break;
                    }

                }
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
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
