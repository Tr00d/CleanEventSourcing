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
            VerifyStatusCode(this.context.CreateItemResponse,
                (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode));

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
            VerifyStatusCode(this.context.GetItemResponse,
                (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode));

        private static void VerifyStatusCode(HttpResponseMessage response, HttpStatusCode expectedStatusCode) =>
            response.StatusCode.Should().Be(expectedStatusCode);

        [When(@"a user gets the created item using the location header")]
        public async Task WhenAUserGetsTheCreatedItemUsingTheLocationHeader() =>
            await this.driver.GetItemUsingLocationHeader();

        [When(@"a user updates the created item with an empty id")]
        public async Task WhenAUserUpdatesTheCreatedItemWithAnEmptyId() =>
            await this.driver.UpdateItem(Guid.Empty, string.Empty);

        [Then(@"the update response should return a ""(.*)"" status code")]
        public void ThenTheUpdateResponseShouldReturnAStatusCode(string statusCode) => VerifyStatusCode(
            this.context.UpdateItemResponse,
            (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode));

        [When(@"a user updates the created item with the description ""(.*)""")]
        public async Task WhenAUserUpdatesTheCreatedItemWithTheDescription(string description) =>
            await this.driver.UpdateItem(await this.context.GetCreatedIdAsync(), description);

        [Then(@"the retrieved item should have the description ""(.*)""")]
        public async Task ThenTheRetrievedItemShouldHaveTheDescription(string description) =>
            (await this.driver.GetRetrievedItemAsync()).Description.Should().Be(description);

        [When(@"a user deletes the created item")]
        public async Task WhenAUserDeletesTheCreatedItem() =>
            await this.driver.DeleteItem(await this.context.GetCreatedIdAsync());

        [Then(@"the deletion response should return a ""(.*)"" status code")]
        public void ThenTheDeletionResponseShouldReturnAStatusCode(string statusCode) => VerifyStatusCode(
            this.context.DeleteItemResponse,
            (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode));
    }
}