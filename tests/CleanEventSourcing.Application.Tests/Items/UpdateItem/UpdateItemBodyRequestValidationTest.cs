using System.Threading.Tasks;
using CleanEventSourcing.Application.Items.UpdateItem;
using FluentValidation.TestHelper;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.UpdateItem
{
    public class UpdateItemBodyRequestValidationTest
    {
        [Fact]
        public void Validate_ShouldHaveErrors_GivenDescriptionIsNull()
        {
            var request = new UpdateItemBodyRequest();
            var validator = new UpdateItemBodyRequestValidation();
            var result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }

        [Fact]
        public async Task ValidateAsync_ShouldHaveErrors_GivenDescriptionIsNull()
        {
            var request = new UpdateItemBodyRequest();
            var validator = new UpdateItemBodyRequestValidation();
            var result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }

        [Fact]
        public void Validate_ShouldHaveErrors_GivenDescriptionIsEmpty()
        {
            var request = new UpdateItemBodyRequest { Description = string.Empty };
            var validator = new UpdateItemBodyRequestValidation();
            var result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }

        [Fact]
        public async Task ValidateAsync_ShouldHaveErrors_GivenDescriptionIsEmpty()
        {
            var request = new UpdateItemBodyRequest { Description = string.Empty };
            var validator = new UpdateItemBodyRequestValidation();
            var result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }
    }
}