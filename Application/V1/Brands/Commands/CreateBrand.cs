using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Brands.Commands
{
    public static class CreateBrand
    {
        public record Command : IRequest<Response>
        {
            public string Name { get; init; }
            public string Country { get; init; }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var brand = new Brand
                {
                    Name = request.Name,
                    Country = request.Country
                };

                _context.Brands.Add(brand);

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Response>(brand);
            }
        }

        public record Response : IMapFrom<Brand>
        {
            public int Id { get; init; }
            public string Name { get; init; }
            public string Country { get; init; }
        }
    }
}
