using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Posts.Commands
{
    public static class DeletePost
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

                _context.Posts.Remove(post);

                var result = await _context.SaveChangesAsync(cancellationToken);

                return new Response(result);
            }
        }

        public record Response(int Id) : CQRSResponse;
    }
}
