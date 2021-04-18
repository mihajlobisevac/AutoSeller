using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
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
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                var posts = await _context.Posts
                    .Where(x => x.Model.Id == request.ModelId)
                    .Include(x => x.Model)
                    .ThenInclude(x => x.Brand)
                    .OrderByDescending(x => x.Created)
                    .ProjectTo<Response>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return posts;
            }
        }

        public record Response : IMapFrom<Post>
        {
            public int Id { get; init; }
            public string Title { get; init; }
            public int VehicleYear { get; init; }
            public string VehicleBrand { get; init; }
            public string VehicleModel { get; init; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Post, Response>()
                    .ForMember(dest => dest.VehicleBrand, opt => opt.MapFrom(src => src.Model.Brand.Name))
                    .ForMember(dest => dest.VehicleModel, opt => opt.MapFrom(src => src.Model.Name));
            }
        }
    }
}
