using GitHubApiCall.Interfaces;
using GitHubApiCall.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GitHubApiCall
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IApiCallService, ApiCallService>()
                .AddSingleton<App>()
                .BuildServiceProvider();

            var app = serviceProvider.GetRequiredService<App>();
            app.Run();
        }
    }
}