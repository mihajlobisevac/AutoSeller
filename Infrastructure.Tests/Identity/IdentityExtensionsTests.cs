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
        public void ToCQRSResponse_ShouldReturnCorrectTypes()
        {
            //arrange
            var identityErrors = new IdentityError[] { new IdentityError() };
            var identityResultFailed = IdentityResult.Failed(identityErrors);
            var identityResultSucceeded = IdentityResult.Success;

            var role = new IdentityRole("RoleName");
            var user = new ApplicationUser();

            //act
            var roleFailed = identityResultFailed.ToCQRSResponse(role);
            var roleSucceeded = identityResultSucceeded.ToCQRSResponse(role);

            var userFailed = identityResultFailed.ToCQRSResponse(user);
            var userSucceeded = identityResultSucceeded.ToCQRSResponse(user);

            //assert
            Assert.True(roleFailed is CQRSResponse);
            Assert.True(roleSucceeded is IdentityRoleResponse);
            Assert.True(userFailed is CQRSResponse);
            Assert.True(userSucceeded is IdentityUserResponse);
        }
    }
}
