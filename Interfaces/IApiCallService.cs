using GitHubApiCall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApiCall.Interfaces
{
    internal interface IApiCallService
    {
        Task<List<GitHubEvent>?> GetUserEventsAsync(string username);
    }
}
