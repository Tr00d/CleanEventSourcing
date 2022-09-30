using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace CleanEventSourcing.Api.Tests.Acceptance.Support;

public class AcceptanceApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    private const string SettingsFile = "appsettings.Acceptance.json";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), SettingsFile))
            .Build();
        builder.UseConfiguration(configurationBuilder);
    }
}