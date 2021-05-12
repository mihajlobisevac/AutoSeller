using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Users.Commands
{
    public static class CreateUser
    {
        public record Command(string Username, string Email, string Password) : IRequest<CQRSResponse>;

        public class Handler : IRequestHandler<Command, CQRSResponse>
        {
            private readonly IIdentityService _identityService;

            public Handler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<CQRSResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var userCreatedResult = await _identityService.CreateUserAsync(
                    request.Username,
                    request.Email,
                    request.Password);

                return userCreatedResult;
            }
        }
    }
}
