using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace CleanEventSourcing.Application.Items.CreateItem
{
    public class CreateItemRequestValidation : AbstractValidator<CreateItemRequest>
    {
        public override ValidationResult Validate(ValidationContext<CreateItemRequest> context)
        {
            Validate();
            return base.Validate(context);
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<CreateItemRequest> context,
            CancellationToken cancellation = new())
        {
            Validate();
            return base.ValidateAsync(context, cancellation);
        }

        private void Validate()
        {
            RuleFor(request => request.Description).NotNull().NotEmpty();
        }
    }
}