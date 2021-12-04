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
            UpdateItemBodyRequest request = new UpdateItemBodyRequest();
            UpdateItemBodyRequestValidation validator = new UpdateItemBodyRequestValidation();
            TestValidationResult<UpdateItemBodyRequest> result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }

        [Fact]
        public async Task ValidateAsync_ShouldHaveErrors_GivenDescriptionIsNull()
        {
            UpdateItemBodyRequest request = new UpdateItemBodyRequest();
            UpdateItemBodyRequestValidation validator = new UpdateItemBodyRequestValidation();
            TestValidationResult<UpdateItemBodyRequest> result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }
        
        [Fact]
        public void Validate_ShouldHaveErrors_GivenDescriptionIsEmpty()
        {
            UpdateItemBodyRequest request = new UpdateItemBodyRequest() { Description = string.Empty};
            UpdateItemBodyRequestValidation validator = new UpdateItemBodyRequestValidation();
            TestValidationResult<UpdateItemBodyRequest> result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }

        [Fact]
        public async Task ValidateAsync_ShouldHaveErrors_GivenDescriptionIsEmpty()
        {
            UpdateItemBodyRequest request = new UpdateItemBodyRequest() { Description = string.Empty};
            UpdateItemBodyRequestValidation validator = new UpdateItemBodyRequestValidation();
            TestValidationResult<UpdateItemBodyRequest> result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }
    }
}