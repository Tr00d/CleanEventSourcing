using System.Threading.Tasks;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.GetItem;
using FluentValidation.TestHelper;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.GetItem
{
    public class GetItemRequestValidationTest
    {
        [Fact]
        public void Validate_ShouldHaveErrors_GivenIdIsEmpty()
        {
            GetItemRequest request = new GetItemRequest();
            GetItemRequestValidation validator = new GetItemRequestValidation();
            TestValidationResult<GetItemRequest> result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(input => input.Id);
        }

        [Fact]
        public async Task ValidateAsync_ShouldHaveErrors_GivenIdIsEmpty()
        {
            GetItemRequest request = new GetItemRequest();
            GetItemRequestValidation validator = new GetItemRequestValidation();
            TestValidationResult<GetItemRequest> result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(input => input.Id);
        }
    }
}