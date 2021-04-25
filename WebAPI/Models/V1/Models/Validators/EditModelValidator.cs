using Application.V1.Models.Commands;
using FluentValidation;

namespace WebAPI.Models.V1.Models.Validators
{
    public class EditModelValidator : AbstractValidator<EditModel.Command>
    {
        public EditModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Model name can't be empty");
        }
    }
}
