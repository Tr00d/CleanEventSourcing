using System;
using System.Net.Http;
using CleanEventSourcing.Api.Tests.Acceptance.Support;
using Microsoft.Extensions.DependencyInjection;

namespace CleanEventSourcing.Api.Tests.Acceptance.Contexts;

public class AcceptanceContext
{
    public AcceptanceContext()
    {
        AcceptanceApplicationFactory<Program> applicationFactory = new AcceptanceApplicationFactory<Program>();
        this.ServiceProvider = applicationFactory.Services.CreateScope().ServiceProvider;
        this.HttpClient = applicationFactory.CreateClient();
    }

    public HttpClient HttpClient { get; init; }

    public IServiceProvider ServiceProvider { get; init; }
}