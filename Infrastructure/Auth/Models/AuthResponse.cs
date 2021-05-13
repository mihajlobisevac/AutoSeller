using Application.Common.Models;

namespace Infrastructure.Auth.Models
{
    public record AuthResponse : CQRSResponse
    {
        public string JwtToken { get; init; }
        public string RefreshToken { get; init; }

        public static AuthResponse Success(string jwtToken, string refreshToken)
            => new()
            {
                JwtToken = jwtToken,
                RefreshToken = refreshToken
            };
    }
}
