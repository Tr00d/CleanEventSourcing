using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace CleanEventSourcing.Application.Items.UpdateItem
{
    public class UpdateItemRouteRequestValidation : AbstractValidator<UpdateItemRouteRequest>
    {
        private void ValidateId() => this.RuleFor(request => request.Id).NotEmpty();

        public override ValidationResult Validate(ValidationContext<UpdateItemRouteRequest> context)
        {
            this.ValidateId();
            return base.Validate(context);
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<UpdateItemRouteRequest> context,
            CancellationToken cancellation = new())
        {
            this.ValidateId();
            return base.ValidateAsync(context, cancellation);
        }
    }
}