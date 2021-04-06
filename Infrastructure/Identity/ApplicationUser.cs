using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public SellerType Type { get; set; }
    }
}
