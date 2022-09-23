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
        public void BeforeScenario(HttpScenarioContext httpContext)
        {
            var testDirectory = Directory.GetCurrentDirectory();
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(testDirectory, "appsettings.Test.json"));
            var server = new TestServer(new WebHostBuilder().UseStartup<Program>()
                .UseConfiguration(configurationBuilder.Build()));
            httpContext.Client = server.CreateClient();
        }
    }
}