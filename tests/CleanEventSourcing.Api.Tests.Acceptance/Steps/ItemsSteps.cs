using System;
using System.Net;
using System.Net.Http;
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
            this.VerifyStatusCode(this.context.CreateItemResponse,
                (HttpStatusCode) Enum.Parse(typeof(HttpStatusCode), statusCode));

        [When(@"a user creates a new item ""(.*)""")]
        [Given(@"a user creates a new item ""(.*)""")]
        public async Task WhenAUserCreatesANewItem(string description) =>
            await this.driver.CreateItem(description).ConfigureAwait(false);

        [Then(@"the creation response contains location header for retrieving the item")]
        public void ThenTheCreationResponseContainsLocationHeaderForRetrievingTheItem() =>
            this.context.CreateItemResponse.Headers.Location.Should().NotBeNull();

        [When(@"I retrieve the item using an empty id")]
        public async Task WhenIRetrieveTheItemUsingAnEmptyId() =>
            await this.driver.GetItem(Guid.Empty).ConfigureAwait(false);

        [Then(@"the retrieval response should return a ""(.*)"" status code")]
        public void ThenTheRetrievalResponseShouldReturnAStatusCode(string statusCode) =>
            this.VerifyStatusCode(this.context.GetItemResponse,
                (HttpStatusCode) Enum.Parse(typeof(HttpStatusCode), statusCode));

        private void VerifyStatusCode(HttpResponseMessage response, HttpStatusCode expectedStatusCode) =>
            response.StatusCode.Should().Be(expectedStatusCode);

        [When(@"a user gets the created item using the location header")]
        public async Task WhenAUserGetsTheCreatedItemUsingTheLocationHeader() =>
            await this.driver.GetItemUsingLocationHeader();

        [Then(@"the created item should have the description ""(.*)""")]
        public async Task ThenTheCreatedItemShouldHaveTheDescription(string description) =>
            (await this.driver.GetCreatedItemAsync()).Description.Should().Be(description);
    }
}