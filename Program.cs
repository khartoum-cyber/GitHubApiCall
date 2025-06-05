using GitHubApiCall.Interfaces;
using GitHubApiCall.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GitHubApiCall
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddHttpClient<IApiCallService, ApiCallService>(client =>
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("request");
                });
                
            services.AddSingleton<App>();
                
            var serviceProvider = services.BuildServiceProvider();

            var app = serviceProvider.GetRequiredService<App>();
            app.Run();
        }
    }
}