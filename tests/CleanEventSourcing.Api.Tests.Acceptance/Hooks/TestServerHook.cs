using System.IO;
using CleanEventSourcing.Api.Tests.Acceptance.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace CleanEventSourcing.Api.Tests.Acceptance.Hooks
{
    [Binding]
    public class TestServerHook
    {
        [BeforeScenario]
        public void BeforeScenario(ServiceProviderContext context, HttpScenarioContext httpContext)
        {
            string testDirectory = Directory.GetCurrentDirectory();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(testDirectory, "appsettings.Test.json"));
            TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>()
                .UseConfiguration(configurationBuilder.Build()));
            context.Provider = server.Services;
            httpContext.Client = server.CreateClient();
        }
    }
}