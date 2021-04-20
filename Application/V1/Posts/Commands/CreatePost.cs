using Application.Common.Interfaces;
using Application.Common.Models;
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

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var model = await _context.Models.FindAsync(request.ModelId);

                var post = new Post
                {
                    Title = request.Title,
                    Description = request.Description,
                    Location = request.Location,
                    Year = request.Year,
                    Mileage = request.Mileage,
                    Engine = request.Engine,
                    Equipment = request.Equipment,
                    Model = model,
                };

                post.SetDrivetrain(request.Drivetrain);
                post.SetTransmission(request.Transmission);
                post.SetBodyStyle(request.Body);

                _context.Posts.Add(post);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response(post.Id);
            }
        }

        public record Response(int Id) : CQRSResponse;
    }
}
