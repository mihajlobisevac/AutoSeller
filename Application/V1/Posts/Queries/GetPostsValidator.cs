using Application.Common.Interfaces;
using Application.Common.Validation;
using Domain.Enums;
using System;
using System.Threading.Tasks;

namespace Application.V1.Posts.Queries
{
    public class GetPostsValidator : IValidationHandler<GetPosts.Query>
    {
        public async Task<ValidationResult> Validate(GetPosts.Query request)
        {
            foreach (var drivetrain in request.Drivetrains)
            {
                if (!Enum.IsDefined(typeof(Drivetrain), drivetrain)) 
                    return ValidationResult.Fail($"One or more invalid drivetrains");
            }

            foreach (var transmission in request.Transmissions)
            {
                if (!Enum.IsDefined(typeof(Transmission), transmission)) 
                    return ValidationResult.Fail($"One or more invalid transmissions");
            }

            foreach (var bodystyle in request.BodyStyles)
            {
                if (!Enum.IsDefined(typeof(BodyStyle), bodystyle)) 
                    return ValidationResult.Fail($"One or more invalid body styles");
            }

            return ValidationResult.Success;
        }
    }
}
