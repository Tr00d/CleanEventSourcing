using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace CleanEventSourcing.Application.Items.DeleteItem
{
    public class DeleteItemRequestValidation : AbstractValidator<DeleteItemRequest>
    {
        public override ValidationResult Validate(ValidationContext<DeleteItemRequest> context)
        {
            this.ValidateId();
            return base.Validate(context);
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<DeleteItemRequest> context,
            CancellationToken cancellation = new())
        {
            this.ValidateId();
            return base.ValidateAsync(context, cancellation);
        }

        private void ValidateId()
        {
            this.RuleFor(request => request.Id).NotEmpty();
        }
    }
}