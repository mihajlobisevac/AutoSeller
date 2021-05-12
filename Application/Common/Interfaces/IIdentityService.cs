using Application.Common.Models;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<CQRSResponse> CreateUserAsync(string username, string email, string password);
        Task<bool> EmailAvailableAsync(string email);
        Task<bool> CheckCredentialsAsync(string email, string password);
        Task<CQRSResponse> CreateRoleAsync(string roleName);
        Task<CQRSResponse> AddUserToRoleAsync(string email, string roleName);
    }
}
