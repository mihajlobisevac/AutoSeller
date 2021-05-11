using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Users.Commands
{
    public static class LoginUser
    {
        public record Command(string Email, string Password) : IRequest<CQRSResponse>;

        public class Handler : IRequestHandler<Command, CQRSResponse>
        {
            private readonly IIdentityService _identityService;
            private readonly IAuthService _authService;

            public Handler(IIdentityService identityService, IAuthService authService)
            {
                _identityService = identityService;
                _authService = authService;
            }

            public async Task<CQRSResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var validCredentials = await _identityService.CheckCredentialsAsync(request.Email, request.Password);

                if (validCredentials == false) return new CQRSResponse("Invalid credentials");

                var tokenResult = await _authService.GenerateJwtTokens(request.Email);

                return tokenResult;
            }
        }
    }
}
