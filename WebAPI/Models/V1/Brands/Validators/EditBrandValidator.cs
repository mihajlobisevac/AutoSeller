using Application.V1.Brands.Commands;
using FluentValidation;

namespace WebAPI.Models.V1.Brands.Validators
{
    public class EditBrandValidator : AbstractValidator<EditBrand.Command>
    {
        public EditBrandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Brand name can't be empty.");
        }
    }
}
