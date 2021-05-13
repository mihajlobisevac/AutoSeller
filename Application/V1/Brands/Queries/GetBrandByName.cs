using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Brands.Queries
{
    public static class GetBrandByName
    {
        public record Query(string BrandName) : IRequest<Response>;

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var brand = await _context.Brands
                    .Include(x => x.Models)
                    .FirstOrDefaultAsync(x => x.Name == request.BrandName, cancellationToken);

                if (brand is null) return null;

                return _mapper.Map<Response>(brand);
            }
        }

        public record Response : IMapFrom<Brand>
        {
            public int Id { get; init; }
            public string Name { get; init; }
            public string Country { get; init; }
            public ICollection<ModelDto> Models { get; init; }
        }

        public record ModelDto : IMapFrom<Model>
        {
            public int Id { get; init; }
            public string Name { get; init; }
        }
    }
}
