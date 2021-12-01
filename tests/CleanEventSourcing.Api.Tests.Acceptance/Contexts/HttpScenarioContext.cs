using System.Net.Http;

namespace CleanEventSourcing.Api.Tests.Acceptance.Contexts
{
    public class HttpScenarioContext
    {
        public HttpClient Client { get; set; }
    }
}