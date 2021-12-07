using System.Threading.Tasks;
using CleanEventSourcing.Application.Items.DeleteItem;
using FluentValidation.TestHelper;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.DeleteItem
{
    public class DeleteItemRequestValidationTest
    {
        [Fact]
        public void Validate_ShouldHaveErrors_GivenIdIsEmpty()
        {
            DeleteItemRequest request = new DeleteItemRequest();
            DeleteItemRequestValidation validator = new DeleteItemRequestValidation();
            TestValidationResult<DeleteItemRequest> result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(input => input.Id);
        }

        [Fact]
        public async Task ValidateAsync_ShouldHaveErrors_GivenIdIsEmpty()
        {
            DeleteItemRequest request = new DeleteItemRequest();
            DeleteItemRequestValidation validator = new DeleteItemRequestValidation();
            TestValidationResult<DeleteItemRequest> result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(input => input.Id);
        }
    }
}