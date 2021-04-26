using Application.V1.Models.Commands;
using FluentValidation;

namespace WebAPI.Models.V1.Models.Validators
{
    public class CreateModelValidator : AbstractValidator<CreateModel.Command>
    {
        public CreateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Model name can't be empty");

            RuleFor(x => x.BrandId)
                .NotEmpty()
                .WithMessage("Brand Id can't be empty or 0")
                .GreaterThan(0)
                .WithMessage("Brand Id has to be greater than 0");
        }
    }
}
