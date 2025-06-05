using GitHubApiCall.Interfaces;
using GitHubApiCall.Models;
using GitHubApiCall.Models.GitHubProfile;
using Newtonsoft.Json;

namespace GitHubApiCall.Services
{
    internal class ApiCallService : IApiCallService
    {

        private readonly HttpClient _httpClient;

        public ApiCallService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GitHubEvent>?> GetUserEventsAsync(string username)
        {
            var url = $"https://api.github.com/users/{username}/events";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

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

        public async Task<GitHubProfile?> GetUserProfileAsync(string username)
        {
            var url = $"https://api.github.com/users/{username}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var profile = JsonConvert.DeserializeObject<GitHubProfile>(responseBody);

                return profile;
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }
    }
}
