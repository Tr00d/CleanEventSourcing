using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace CleanEventSourcing.Application.Items.GetItem
{
    public class GetItemRequestValidation : AbstractValidator<GetItemRequest>
    {
        public override ValidationResult Validate(ValidationContext<GetItemRequest> context)
        {
            this.ValidateId();
            return base.Validate(context);
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<GetItemRequest> context,
            CancellationToken cancellation = new())
        {
            this.ValidateId();
            return base.ValidateAsync(context, cancellation);
        }

        private void ValidateId() => this.RuleFor(request => request.Id).NotEmpty();
    }
}