using Application.Common.Models;

namespace Infrastructure.Identity.Models
{
    public record IdentityUserResponse : CQRSResponse
    {
        public string UserId { get; init; }
        public string Email { get; init; }

        public static new IdentityUserResponse Success(ApplicationUser user)
            => new()
            {
                UserId = user.Id,
                Email = user.Email
            };
    }
}
