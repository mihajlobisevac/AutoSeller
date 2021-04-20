using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Brands.Queries
{
    public static class GetBrands
    {
        public record Query : IRequest<IEnumerable<Response>>;

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
                var brands = await _context.Brands
                    .ProjectTo<Response>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return brands;
            }
        }

        public record Response : CQRSResponse, IMapFrom<Brand>
        {
            public int Id { get; init; }
            public string Name { get; init; }
            public string Country { get; init; }
        }
    }
}
