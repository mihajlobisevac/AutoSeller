using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(string email, string username)
        {
            Email = email;
            UserName = username;
        }

        public ApplicationUser()
        {
        }
    }
}
