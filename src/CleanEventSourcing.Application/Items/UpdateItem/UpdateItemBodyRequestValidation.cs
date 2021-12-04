using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace CleanEventSourcing.Application.Items.UpdateItem
{
    public class UpdateItemBodyRequestValidation : AbstractValidator<UpdateItemBodyRequest>
    {
        private void ValidateDescription() => this.RuleFor(request => request.Description).NotNull().NotEmpty();

        public override ValidationResult Validate(ValidationContext<UpdateItemBodyRequest> context)
        {
            this.ValidateDescription();
            return base.Validate(context);
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<UpdateItemBodyRequest> context, CancellationToken cancellation = new CancellationToken())
        {
            this.ValidateDescription();
            return base.ValidateAsync(context, cancellation);
        }
    }
}