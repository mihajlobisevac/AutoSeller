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
            private readonly IAuthService _authService;

            public Handler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<CQRSResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _authService.GenerateJwtTokens(request.Email);
            }
        }
    }
}
