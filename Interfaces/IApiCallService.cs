using GitHubApiCall.Models;
using GitHubApiCall.Models.GitHubProfile;

namespace GitHubApiCall.Interfaces
{
    internal interface IApiCallService
    {
        Task<List<GitHubEvent>?> GetUserEventsAsync(string username);
        Task<GitHubProfile?> GetUserProfileAsync(string username);
    }
}
