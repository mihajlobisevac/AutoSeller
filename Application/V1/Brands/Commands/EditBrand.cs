using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
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
                var brand = await _context.Brands.FindAsync(request.Id);

                brand.Name = request.Name;
                brand.Country = request.Country;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Response>(brand);
            }
        }

        public record Response : CQRSResponse, IMapFrom<Brand>
        {
            public int Id { get; init; }
            public string Name { get; init; }
            public string Country { get; init; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Brand, Response>()
                    .ForMember(dest => dest.IsSuccessful, opt => opt.Ignore())
                    .ForMember(dest => dest.Errors, opt => opt.Ignore());
            }
        }
    }
}
