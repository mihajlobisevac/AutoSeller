using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Posts.Queries
{
    public static class GetPost
    {
        public record Query(int PostId) : IRequest<Response>;

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
                var post = await _context.Posts
                    .Include(x => x.Model)
                    .ThenInclude(x => x.Brand)
                    .FirstOrDefaultAsync(x => x.Id == request.PostId);

                if (post is null) return null;

                return _mapper.Map<Response>(post);
            }
        }

        public record Response : CQRSResponse, IMapFrom<Post>
        {
            public int Id { get; init; }
            public string Title { get; init; }
            public string Description { get; init; }
            public string Location { get; init; }
            public int Year { get; init; }
            public int Mileage { get; init; }
            public string Engine { get; init; }
            public string Drivetrain { get; init; }
            public string Transmission { get; init; }
            public string Body { get; init; }
            public string Equipment { get; init; }
            public string VehicleBrand { get; init; }
            public string VehicleModel { get; init; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Post, Response>()
                    .ForMember(dest => dest.Drivetrain, opt => opt.MapFrom(src => src.Drivetrain.ToString()))
                    .ForMember(dest => dest.Transmission, opt => opt.MapFrom(src => src.Transmission.ToString()))
                    .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body.ToString()))
                    .ForMember(dest => dest.VehicleBrand, opt => opt.MapFrom(src => src.Model.Brand.Name))
                    .ForMember(dest => dest.VehicleModel, opt => opt.MapFrom(src => src.Model.Name));
            }
        }
    }
}
