using Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Infrastructure.Identity
{
    public static class IdentityExtensions
    {
        public static CQRSResponse ToCQRSResponse(this IdentityResult identityResult, IdentityRole role)
        {
            if (identityResult.Succeeded == false)
            {
                var errors = identityResult.Errors.Select(x => x.Description).ToArray();
                return CQRSResponse.Fail(errors);
            }

            return IdentityRoleResponse.Success(role);
        }

        public static CQRSResponse ToCQRSResponse(this IdentityResult identityResult, ApplicationUser user)
        {
            if (identityResult.Succeeded == false)
            {
                var errors = identityResult.Errors.Select(x => x.Description).ToArray();
                return CQRSResponse.Fail(errors);
            }

            return IdentityUserResponse.Success(user);
        }
    }
}
