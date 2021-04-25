using Application.V1.Brands.Commands;
using FluentValidation;

namespace WebAPI.Models.V1.Brands.Validators
{
    public class CreateBrandValidator : AbstractValidator<CreateBrand.Command>
    {
        public CreateBrandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Brand name can't be empty.");
        }
    }
}
