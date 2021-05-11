using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Users.Commands
{
    public static class RefreshUserToken
    {
        public record Command(string JwtToken, string RefreshToken) : IRequest<CQRSResponse>;

        public class Handler : IRequestHandler<Command, CQRSResponse>
        {
            private readonly IAuthService _authService;

            public Handler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<CQRSResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _authService.ValidateAndCreateTokensAsync(
                    request.JwtToken,
                    request.RefreshToken);

                if (result is null) return new CQRSResponse("Failed validating tokens");

                return result;
            }
        }
    }
}
