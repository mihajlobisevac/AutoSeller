using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Users.Commands
{
    public static class CreateUser
    {
        public record Command : IRequest<Response>
        {
            public string Username { get; init; }
            public string Email { get; init; }
            public string Password { get; init; }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IIdentityService _identityService;

            public Handler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _identityService.CreateUserAsync(
                    request.Username,
                    request.Email,
                    request.Password);

                if (result.IsSuccessful) return new Response(request.Username, request.Email);

                return new Response("Unable to register");
            }
        }

        public record Response : CQRSResponse 
        {
            public string Username { get; init; }
            public string Email { get; init; }

            public Response(string error)
            {
                IsSuccessful = false;
                ErrorMessage = error;
            }

            public Response(string username, string email)
            {
                Username = username;
                Email = email;
            }
        }
    }
}
