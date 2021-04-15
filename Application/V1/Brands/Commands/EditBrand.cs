using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Brands.Commands
{
    public static class EditBrand
    {
        public record Command : IRequest<Response>
        {
            public int Id { get; init; }
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
                var brand = await _context.Brands
                    .FindAsync(request.Id);

                brand.Name = request.Name;
                brand.Country = request.Country;

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
