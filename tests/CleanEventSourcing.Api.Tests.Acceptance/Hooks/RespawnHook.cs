using System.Threading.Tasks;
using CleanEventSourcing.Api.Tests.Acceptance.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using TechTalk.SpecFlow;

namespace CleanEventSourcing.Api.Tests.Acceptance.Hooks;

[Binding]
public class RespawnHook
{
    private static Checkpoint? checkpoint;
    private static string connectionString = string.Empty;
    private readonly AcceptanceContext context;

    public RespawnHook(AcceptanceContext context)
    {
        this.context = context;
    }

    [BeforeTestRun]
    public static void CreateCheckpointBeforeTestRun() => checkpoint = new Checkpoint();

    [BeforeScenario]
    public async Task RespawnDatabaseBeforeScenario()
    {
        var configuration = this.context.ServiceProvider.GetRequiredService<IConfiguration>();
        connectionString = configuration.GetConnectionString("Database");
        await ResetCheckpoint();
    }

    private static async Task ResetCheckpoint()
    {
        if (checkpoint != null)
        {
            await checkpoint.Reset(connectionString);
        }
    }

    [AfterTestRun]
    public static async Task RespawnDatabaseAfterTestRun() => await ResetCheckpoint();
}