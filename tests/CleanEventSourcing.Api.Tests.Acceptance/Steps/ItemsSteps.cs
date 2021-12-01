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
        public void CreateItemShouldReturnStatusCode(string statusCode) => context.CreationResponse.StatusCode.Should()
            .Be(Enum.Parse(typeof(HttpStatusCode), statusCode));

        [When(@"I create a new item ""(.*)""")]
        public async Task CreateItem(string description) => await driver.CreateItem(description).ConfigureAwait(false);
    }
}