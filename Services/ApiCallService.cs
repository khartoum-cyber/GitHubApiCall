using GitHubApiCall.Interfaces;
using GitHubApiCall.Models;
using Newtonsoft.Json;

namespace GitHubApiCall.Services
{
    internal class ApiCallService : IApiCallService
    {
        public async Task<List<GitHubEvent>?> GetUserEventsAsync(string username)
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
    }
}
