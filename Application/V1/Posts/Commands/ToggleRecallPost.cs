using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Posts.Commands
{
    public static class ToggleRecallPost
    {
        public record Command(int PostId) : IRequest<int>;

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var post = await _context.Posts.FindAsync(request.PostId);

                post.IsRecalled = !post.IsRecalled;

                return await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
