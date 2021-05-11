using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Roles.Commands
{
    public class CreateRole
    {
        public record Command(string RoleName) : IRequest<CQRSResponse>;

        public class Handler : IRequestHandler<Command, CQRSResponse>
        {
            private readonly IIdentityService _identityService;

            public Handler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<CQRSResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _identityService.CreateRoleAsync(request.RoleName);
            }
        }
    }
}
