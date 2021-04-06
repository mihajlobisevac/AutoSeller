using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Brand> Brands { get; set; }
        DbSet<Model> Models { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Vehicle> Vehicles { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
