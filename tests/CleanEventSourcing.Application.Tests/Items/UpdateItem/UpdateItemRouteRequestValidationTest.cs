using System.Threading.Tasks;
using CleanEventSourcing.Application.Items.UpdateItem;
using FluentValidation.TestHelper;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.UpdateItem
{
    public class UpdateItemRouteRequestValidationTest
    {
        [Fact]
        public void Validate_ShouldHaveErrors_GivenIdIsEmpty()
        {
            var request = new UpdateItemRouteRequest();
            var validator = new UpdateItemRouteRequestValidation();
            var result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(input => input.Id);
        }

        [Fact]
        public async Task ValidateAsync_ShouldHaveErrors_GivenIdIsEmpty()
        {
            var request = new UpdateItemRouteRequest();
            var validator = new UpdateItemRouteRequestValidation();
            var result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(input => input.Id);
        }
    }
}