using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Brands.Queries
{
    public static class GetBrands
    {
        public record Query : IRequest<Response>;

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var brands = await _context.Brands
                    .ToListAsync();

                return null;
            }
        }

        public record Response
        {
            public int Id { get; init; }
            public string Name { get; init; }
            public string Country { get; init; }
        }
    }
}
