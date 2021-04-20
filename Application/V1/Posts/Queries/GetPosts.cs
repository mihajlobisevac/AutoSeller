using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Enums;
using Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Posts.Queries
{
    public static class GetPosts
    {
        public record Query : IRequest<PaginatedList<Response>>
        {
            public int[] BrandIds { get; init; }
            public int[] ModelIds { get; init; }
            public string[] Drivetrains { get; init; }
            public string[] Transmissions { get; init; }
            public string[] BodyStyles { get; init; }
            public int? YearLower { get; init; }
            public int? YearUpper { get; init; }
            public int? MileageLower { get; init; }
            public int? MileageUpper { get; init; }
            public int PageNumber { get; set; } = 1;
            public int PageSize { get; set; } = 10;
        }

        public class Handler : IRequestHandler<Query, PaginatedList<Response>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PaginatedList<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                var drivetrains = request.Drivetrains?.ToEnumValueArray<Drivetrain>();
                var transmissions = request.Transmissions?.ToEnumValueArray<Transmission>();
                var bodyStyles = request.BodyStyles?.ToEnumValueArray<BodyStyle>();

                var posts = await _context.Posts
                    .Include(x => x.Model)
                        .ThenInclude(x => x.Brand)
                    .Where(post => post.IsRecalled == false)
                    .Where(post => post.Year >= request.YearLower || request.YearLower == null)
                    .Where(post => post.Year <= request.YearUpper || request.YearUpper == null)
                    .Where(post => post.Mileage >= request.MileageLower || request.MileageLower == null)
                    .Where(post => post.Mileage <= request.MileageUpper || request.MileageUpper == null)
                    .Where(post => request.BrandIds.Any(id => id == post.Model.Brand.Id) || request.BrandIds == null)
                    .Where(post => request.ModelIds.Any(id => id == post.Model.Id) || request.ModelIds == null)
                    .Where(post => drivetrains.Any(dt => (int)post.Drivetrain == dt) || request.Drivetrains == null)
                    .Where(post => transmissions.Any(dt => (int)post.Transmission == dt) || request.Transmissions == null)
                    .Where(post => bodyStyles.Any(dt => (int)post.Body == dt) || request.BodyStyles == null)
                    .ProjectTo<Response>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);

                return posts;
            }
        }

        public record Response : CQRSResponse, IMapFrom<Post>
        {
            public int Id { get; init; }
            public string Title { get; init; }
            public int VehicleYear { get; init; }
            public string VehicleBrand { get; init; }
            public string VehicleModel { get; init; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Post, Response>()
                    .ForMember(dest => dest.VehicleYear, opt => opt.MapFrom(src => src.Year))
                    .ForMember(dest => dest.VehicleBrand, opt => opt.MapFrom(src => src.Model.Brand.Name))
                    .ForMember(dest => dest.VehicleModel, opt => opt.MapFrom(src => src.Model.Name));
            }
        }
    }
}
