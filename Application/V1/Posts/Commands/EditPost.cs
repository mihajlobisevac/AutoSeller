using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Posts.Commands
{
    public static class EditPost
    {
        public record Command : IRequest<Response>
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
            public int ModelId { get; init; }
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
                var post = await _context.Posts
                    .Include(x => x.Model)
                    .ThenInclude(x => x.Brand)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                var model = await _context.Models.FindAsync(request.ModelId);

                post.Title = request.Title;
                post.Description = request.Description;
                post.Location = request.Location;
                post.Year = request.Year;
                post.Mileage = request.Mileage;
                post.Engine = request.Engine;
                post.Equipment = request.Equipment;
                post.Model = model;

                post.SetDrivetrain(request.Drivetrain);
                post.SetTransmission(request.Transmission);
                post.SetBodyStyle(request.Body);

                await _context.SaveChangesAsync(cancellationToken);

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
                    .ForMember(dest => dest.VehicleModel, opt => opt.MapFrom(src => src.Model.Name))
                    .ForMember(dest => dest.IsSuccessful, opt => opt.Ignore())
                    .ForMember(dest => dest.Errors, opt => opt.Ignore());
            }
        }
    }
}
