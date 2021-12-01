using System;
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
            CreateItemRequest request = new CreateItemRequest();
            CreateItemRequestValidation validator = new CreateItemRequestValidation();
            TestValidationResult<CreateItemRequest> result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }

        [Fact]
        public async Task ValidateAsync_ShouldHaveErrors_GivenDescriptionIsNull()
        {
            CreateItemRequest request = new CreateItemRequest();
            CreateItemRequestValidation validator = new CreateItemRequestValidation();
            TestValidationResult<CreateItemRequest> result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }

        [Fact]
        public void Validate_ShouldHaveErrors_GivenDescriptionIsEmpty()
        {
            CreateItemRequest request = new CreateItemRequest {Description = String.Empty};
            CreateItemRequestValidation validator = new CreateItemRequestValidation();
            TestValidationResult<CreateItemRequest> result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }

        [Fact]
        public async Task ValidateAsync_ShouldHaveErrors_GivenDescriptionIsEmpty()
        {
            CreateItemRequest request = new CreateItemRequest{Description = String.Empty};
            CreateItemRequestValidation validator = new CreateItemRequestValidation();
            TestValidationResult<CreateItemRequest> result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(input => input.Description);
        }
    }
}