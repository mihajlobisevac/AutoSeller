using Application.Common.Interfaces;
using Application.Common.Validation;
using Application.V1.Users.Commands;
using System.Threading.Tasks;

namespace Application.V1.Users.Validators
{
    public class LoginUserValidator : IValidationHandler<LoginUser.Command>
    {
        private readonly IIdentityService _identityService;

        public LoginUserValidator(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ValidationResult> Validate(LoginUser.Command request)
        {
            var validCredentials = await _identityService.CheckCredentialsAsync(request.Email, request.Password);
            if (validCredentials == false) return ValidationResult.Fail("Invalid credentials");

            return ValidationResult.Success;
        }
    }
}
