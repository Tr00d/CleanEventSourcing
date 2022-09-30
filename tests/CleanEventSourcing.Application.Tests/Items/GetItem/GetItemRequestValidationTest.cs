using System.Threading.Tasks;
using CleanEventSourcing.Application.Items.GetItem;
using FluentValidation.TestHelper;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.GetItem
{
    public class GetItemRequestValidationTest
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void Validate_ShouldHaveErrors_GivenIdIsEmpty()
        {
            var request = new GetItemRequest();
            var validator = new GetItemRequestValidation();
            var result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(input => input.Id);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task ValidateAsync_ShouldHaveErrors_GivenIdIsEmpty()
        {
            var request = new GetItemRequest();
            var validator = new GetItemRequestValidation();
            var result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(input => input.Id);
        }
    }
}