using GitHubApiCall.Helpers;
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

            while (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Enter GitHub username: ");

                username = Console.ReadLine();

                if (string.IsNullOrEmpty(username))
                {
                    Console.WriteLine("Username cannot be empty. Please try again.");
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
                    Console.WriteLine(element.Type);
                }
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
        }
    }

    public class GitHubEvent
    {
        public string Type { get; set; }
    }
}
