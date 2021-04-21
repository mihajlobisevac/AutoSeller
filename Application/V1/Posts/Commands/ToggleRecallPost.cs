using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Posts.Commands
{
    public static class ToggleRecallPost
    {
        public record Command(int PostId) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var post = await _context.Posts.FindAsync(request.PostId);

                post.IsRecalled = !post.IsRecalled;

                var result = await _context.SaveChangesAsync(cancellationToken);

                return new Response { Id = result };
            }
        }

        public record Response : CQRSResponse
        {
            public int Id { get; init; }
        }
    }
}
