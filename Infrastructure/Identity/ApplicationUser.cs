using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public SellerType SellerType { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
