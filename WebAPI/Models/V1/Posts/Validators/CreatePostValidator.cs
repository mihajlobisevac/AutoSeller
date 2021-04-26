using Application.V1.Posts.Commands;
using FluentValidation;

namespace WebAPI.Models.V1.Posts.Validators
{
    public class CreatePostValidator : AbstractValidator<CreatePost.Command>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title can't be empty");

            RuleFor(x => x.Year)
                .NotEmpty()
                .WithMessage("Year can't be empty or 0")
                .GreaterThan(0)
                .WithMessage("Year has to be greater than 0");

            RuleFor(x => x.Mileage)
                .NotEmpty()
                .WithMessage("Mileage can't be empty or 0")
                .GreaterThan(0)
                .WithMessage("Mileage has to be greater than 0");

            RuleFor(x => x.ModelId)
                .NotEmpty()
                .WithMessage("Model Id can't be empty or 0")
                .GreaterThan(0)
                .WithMessage("Model Id has to be greater than 0");
        }
    }
}
