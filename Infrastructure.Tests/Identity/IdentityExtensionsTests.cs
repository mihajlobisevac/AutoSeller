using Application.Common.Models;
using Infrastructure.Identity;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace Infrastructure.Tests.Identity
{
    public class IdentityExtensionsTests
    {
        [Fact]
        public void ToCQRSResponse_ForIdentityRole()
        {
            //arrange
            var identityErrors = new IdentityError[] { new IdentityError() };
            var identityResultFailed = IdentityResult.Failed(identityErrors);
            var identityResultSucceeded = IdentityResult.Success;
            var role = new IdentityRole("RoleName");

            //act
            var roleFailed = identityResultFailed.ToCQRSResponse(role);
            var roleSucceeded = identityResultSucceeded.ToCQRSResponse(role);

            //assert
            Assert.IsType<CQRSResponse>(roleFailed);
            Assert.IsType<IdentityRoleResponse>(roleSucceeded);
        }

        [Fact]
        public void ToCQRSResponse_ForApplicationUser()
        {
            //arrange
            var identityErrors = new IdentityError[] { new IdentityError() };
            var identityResultFailed = IdentityResult.Failed(identityErrors);
            var identityResultSucceeded = IdentityResult.Success;
            var user = new ApplicationUser();

            //act
            var userFailed = identityResultFailed.ToCQRSResponse(user);
            var userSucceeded = identityResultSucceeded.ToCQRSResponse(user);

            //assert
            Assert.IsType<CQRSResponse>(userFailed);
            Assert.IsType<IdentityUserResponse>(userSucceeded);
        }
    }
}
