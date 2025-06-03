using GitHubApiCall.Models;

namespace GitHubApiCall.Interfaces
{
    internal interface IApiCallService
    {
        Task<List<GitHubEvent>?> GetUserEventsAsync(string username);
        Task<List<GitHubProfile>?> GetUserProfileAsync(string username);
    }
}
