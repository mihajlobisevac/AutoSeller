using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Models.Commands
{
    public static class EditModel
    {
        public record Command : IRequest<Response>
        {
            public int Id { get; init; }
            public string Name { get; init; }
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
                var model = await _context.Models
                    .Include(x => x.Brand)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                model.Name = request.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Response>(model);
            }
        }

        public record Response : CQRSResponse, IMapFrom<Model>
        {
            public int Id { get; init; }
            public string Name { get; init; }
            public string BrandName { get; init; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Model, Response>()
                    .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));
            }
        }
    }
}
