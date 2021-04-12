using Application.Common.Models;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Result> CreateUserAsync(string username, string email, string password);
        Task<bool> EmailAvailableAsync(string email);
    }
}
