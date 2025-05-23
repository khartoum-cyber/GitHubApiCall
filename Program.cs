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

        static async Task PrintUserEvents()
        {
            var username = Helper.PromptForUsername();
            if (username == null)
                return;

            var events = await GetUserEventsAsync(username);

            if (events == null)
            {
                Console.WriteLine("No events retrieved.");
                return;
            }

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
