using Application.Common.Interfaces;
using Application.Common.Models;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<CQRSResponse> AddUserToRoleAsync(string email, string roleName)
        {
            List<string> errors = new();

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists == false) errors.Add($"Role with name '{roleName}' not found");

            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) errors.Add($"User with email '{email}' not found");

            var isInRole = await _userManager.IsInRoleAsync(user, roleName);
            if (isInRole == true) errors.Add($"User '{email}' is already assigned to role '{roleName}'");

            var roleResult = await _userManager.AddToRoleAsync(user, roleName);
            if (roleResult.Succeeded) return CQRSResponse.Success;

            errors.Add($"Unable to assign role '{roleName}' to user '{email}'");

            return CQRSResponse.Fail(errors.ToArray());
        }

        public async Task<bool> CheckCredentialsAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<CQRSResponse> CreateRoleAsync(string roleName)
        {
            var role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);

            return result.ToCQRSResponse(role);
        }

        public async Task<CQRSResponse> CreateUserAsync(string username, string email, string password)
        {
            var user = new ApplicationUser(email, username);
            var result = await _userManager.CreateAsync(user, password);

            return result.ToCQRSResponse(user);
        }

        public async Task<bool> EmailAvailableAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is null;
        }
    }
}
