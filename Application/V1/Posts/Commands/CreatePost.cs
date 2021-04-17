using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Posts.Commands
{
    public static class CreatePost
    {
        public record Command : IRequest<Response>
        {
            public string Title { get; init; }
            public string Description { get; init; }
            public string Location { get; init; }
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
                //var brand = await _context.Brands.FindAsync(request.BrandId);

                //var model = new Model
                //{
                //    Name = request.Name,
                //    Brand = brand
                //};

                //_context.Models.Add(model);

                //await _context.SaveChangesAsync(cancellationToken);

                return null;
            }
        }

        public record Response : IMapFrom<Model>
        {
            public int Id { get; init; }
            public string Name { get; init; }
        }
    }
}
