using System.Threading.Tasks;
using CleanEventSourcing.Application.Items.CreateItem;
using FluentValidation.TestHelper;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.CreateItem
{
    public class CreateItemRequestValidationTest
    {
        [Fact]
        public void Validate_ShouldHaveErrors_GivenDescriptionIsNull()
        {
            var request = new CreateItemRequest();
            var validator = new CreateItemRequestValidation();
            var result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }

        [Fact]
        public async Task ValidateAsync_ShouldHaveErrors_GivenDescriptionIsNull()
        {
            var request = new CreateItemRequest();
            var validator = new CreateItemRequestValidation();
            var result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }

        [Fact]
        public void Validate_ShouldHaveErrors_GivenDescriptionIsEmpty()
        {
            var request = new CreateItemRequest { Description = string.Empty };
            var validator = new CreateItemRequestValidation();
            var result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }

        [Fact]
        public async Task ValidateAsync_ShouldHaveErrors_GivenDescriptionIsEmpty()
        {
            var request = new CreateItemRequest { Description = string.Empty };
            var validator = new CreateItemRequestValidation();
            var result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }
    }
}