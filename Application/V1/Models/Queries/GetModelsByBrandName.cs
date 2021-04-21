using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Models.Queries
{
    public static class GetModelsByBrandName
    {
        public record Query(string BrandName) : IRequest<IEnumerable<Response>>;

        public class Handler : IRequestHandler<Query, IEnumerable<Response>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Models
                    .Where(x => x.Brand.Name == request.BrandName)
                    .ProjectTo<Response>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
        }

        public record Response : CQRSResponse, IMapFrom<Model>
        {
            public int Id { get; init; }
            public string Name { get; init; }
        }
    }
}
