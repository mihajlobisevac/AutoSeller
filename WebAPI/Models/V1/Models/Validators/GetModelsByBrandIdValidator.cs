using Application.V1.Models.Queries;
using FluentValidation;

namespace WebAPI.Models.V1.Models.Validators
{
    public class GetModelsByBrandIdValidator : AbstractValidator<GetModelsByBrandId.Query>
    {
        public GetModelsByBrandIdValidator()
        {
            RuleFor(x => x.BrandId)
                .NotEmpty()
                .WithMessage("Brand Id can't be empty or 0")
                .GreaterThan(0)
                .WithMessage("Brand Id has to be greater than 0");
        }
    }
}
