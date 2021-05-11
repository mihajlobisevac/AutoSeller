using Application.Common.Models;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<CQRSResponse> GenerateJwtTokens(string email);
        Task<CQRSResponse> ValidateAndCreateTokensAsync(string jwtToken, string refreshToken);
    }
}
