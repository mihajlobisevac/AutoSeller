using Application.Common.Models;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Infrastructure.Identity
{
    public static class IdentityExtensions
    {
        public static CQRSResponse ToCQRSResponse(this IdentityResult identityResult, IdentityRole role)
        {
            return identityResult.Succeeded == false
                ? identityResult.Failed()
                : IdentityRoleResponse.Success(role);
        }

        public static CQRSResponse ToCQRSResponse(this IdentityResult identityResult, ApplicationUser user)
        {
            return identityResult.Succeeded == false
                ? identityResult.Failed()
                : IdentityUserResponse.Success(user);
        }

        private static CQRSResponse Failed(this IdentityResult identityResult)
        {
            var errors = identityResult.Errors.Select(x => x.Description).ToArray();
            return CQRSResponse.Fail(errors);
        }
    }
}
