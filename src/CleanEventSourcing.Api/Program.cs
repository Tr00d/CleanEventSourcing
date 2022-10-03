using CleanEventSourcing.Application;
using CleanEventSourcing.Infrastructure;
using CleanEventSourcing.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceCollectionExtension = CleanEventSourcing.Application.ServiceCollectionExtension;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting();
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddControllers();
builder.Services.AddControllers().AddFluentValidation(configuration =>
{
    configuration.RegisterValidatorsFromAssembly(typeof(ServiceCollectionExtension).Assembly);
});
builder.Services.RegisterApplication();
builder.Services.RegisterPersistence(builder.Configuration);
builder.Services.RegisterInfrastructure();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.MapControllers();
EnsureDatabaseCreated(app);
app.Run();

void EnsureDatabaseCreated(IHost webApplication)
{
    var context = webApplication.Services.CreateScope().ServiceProvider.GetRequiredService<Context>();
    context.Database.EnsureCreated();
}

public partial class Program
{
}