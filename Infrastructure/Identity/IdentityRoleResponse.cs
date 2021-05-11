using Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public record IdentityRoleResponse : CQRSResponse
    {
        public string RoleId { get; init; }
        public string RoleName { get; init; }

        public static IdentityRoleResponse Success(IdentityRole role)
            => new()
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
    }
}
