using System.Threading.Tasks;
using CleanEventSourcing.Application.Items.DeleteItem;
using FluentValidation.TestHelper;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.DeleteItem
{
    public class DeleteItemRequestValidationTest
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void Validate_ShouldHaveErrors_GivenIdIsEmpty()
        {
            var request = new DeleteItemRequest();
            var validator = new DeleteItemRequestValidation();
            var result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(input => input.Id);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task ValidateAsync_ShouldHaveErrors_GivenIdIsEmpty()
        {
            var request = new DeleteItemRequest();
            var validator = new DeleteItemRequestValidation();
            var result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(input => input.Id);
        }
    }
}