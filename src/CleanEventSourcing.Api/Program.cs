using CleanEventSourcing.Application;
using CleanEventSourcing.Infrastructure;
using CleanEventSourcing.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting();
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.RegisterApplication();

//builder.Services.RegisterPersistence(this.databaseName);
builder.Services.RegisterInfrastructure();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

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