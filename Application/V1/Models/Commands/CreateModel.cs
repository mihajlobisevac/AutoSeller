using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Models.Commands
{
    public static class CreateModel
    {
        public record Command : IRequest<Response>
        {
            public string Name { get; init; }
            public int BrandId { get; init; }
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
                var model = new Model
                {
                    Name = request.Name,
                    Brand = await _context.Brands.FindAsync(request.BrandId)
                };

                _context.Models.Add(model);

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Response>(model);
            }
        }

        public record Response : CQRSResponse, IMapFrom<Model>
        {
            public int Id { get; init; }
            public string Name { get; init; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Model, Response>()
                    .ForMember(dest => dest.IsSuccessful, opt => opt.Ignore())
                    .ForMember(dest => dest.Errors, opt => opt.Ignore());
            }
        }
    }
}
