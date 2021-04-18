using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Posts.Queries
{
    public static class GetPostsByModelId
    {
        public record Query(int ModelId) : IRequest<IEnumerable<Response>>;

        public class Handler : IRequestHandler<Query, IEnumerable<Response>>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                var posts = await _context.Posts
                    .Where(x => x.Model.Id == request.ModelId)
                    .Include(x => x.Model)
                    .ThenInclude(x => x.Brand)
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        Title = x.Title,
                        VehicleYear = x.Year,
                        VehicleBrand = x.Model.Brand.Name,
                        VehicleModel = x.Model.Name
                    })
                    .ToListAsync(cancellationToken);

                return posts;
            }
        }

        public record Response
        {
            public int Id { get; init; }
            public string Title { get; init; }
            public int VehicleYear { get; init; }
            public string VehicleBrand { get; init; }
            public string VehicleModel { get; init; }
        }
    }
}
