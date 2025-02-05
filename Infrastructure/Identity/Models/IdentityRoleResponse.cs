﻿using Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Models
{
    public record IdentityRoleResponse : CQRSResponse
    {
        public string RoleId { get; init; }
        public string RoleName { get; init; }

        public static new IdentityRoleResponse Success(IdentityRole role)
            => new()
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
    }
}
