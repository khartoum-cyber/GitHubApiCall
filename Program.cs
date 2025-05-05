using Newtonsoft.Json;

namespace GitHubApiCall
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("GitHub User Activity CLI !");

            RunAsync().Wait();
        }

        private static async Task RunAsync()
        {
            string username = string.Empty;

            while(string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Enter GitHub username: ");

                username = Console.ReadLine();

                if (string.IsNullOrEmpty(username))
                {
                    Console.WriteLine("Username cannot be empty. Please try again.");
                }
            }

            var url = $"https://api.github.com/users/{username}/events";

            await GetUserActivityAsync(url);
        }

        static async Task GetUserActivityAsync(string path)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

            try
            {
                HttpResponseMessage response = await client.GetAsync(path);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var events = JsonConvert.DeserializeObject<List<GitHubEvent>>(responseBody);

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
