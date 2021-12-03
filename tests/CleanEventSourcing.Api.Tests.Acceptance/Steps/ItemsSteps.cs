using System;
using System.Net;
using System.Threading.Tasks;
using CleanEventSourcing.Api.Tests.Acceptance.Contexts;
using CleanEventSourcing.Api.Tests.Acceptance.Drivers;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CleanEventSourcing.Api.Tests.Acceptance.Steps
{
    [Binding]
    public class ItemsSteps
    {
        private readonly ItemsContext context;
        private readonly ItemsDriver driver;

        public ItemsSteps(ItemsDriver driver, ItemsContext context)
        {
            this.context = context;
            this.driver = driver;
        }

        [Then(@"the creation response should return a ""(.*)"" status code")]
        public void ThenTheCreationResponseShouldReturnAStatusCode(string statusCode) =>
            this.context.CreationResponse.StatusCode.Should()
            .Be((HttpStatusCode) Enum.Parse(typeof(HttpStatusCode), statusCode));

        [When(@"I create a new item ""(.*)""")]
        public async Task WhenICreateANewItem(string description) => await this.driver.CreateItem(description).ConfigureAwait(false);

        [Then(@"the creation response contains location header for retrieving the item")]
        public void ThenTheCreationResponseContainsLocationHeaderForRetrievingTheItem() => this.context.CreationResponse.Headers.Location.Should().NotBeNull();
    }
}