using System.Net.Http;

namespace CleanEventSourcing.Api.Tests.Acceptance.Contexts
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class HttpScenarioContext
    {
        public HttpClient Client { get; set; }
    }
}