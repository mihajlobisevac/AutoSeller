using Application.Common.Interfaces;
using Application.Common.Validation;
using Application.V1.Posts.Commands;
using System.Threading.Tasks;

namespace Application.V1.Posts.Validators
{
    public class DeletePostValidator : IValidationHandler<DeletePost.Command>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public DeletePostValidator(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<ValidationResult> Validate(DeletePost.Command request)
        {
            var post = await _context.Posts.FindAsync(request.PostId);
            if (post is null)
            {
                return ValidationResult.Fail("Post does not exist");
            }

            if (post.CreatedBy != _currentUser.UserId && _currentUser.IsAdmin == false)
            {
                return ValidationResult.Fail("Unauthorized to delete this post");
            }

            return ValidationResult.Success;
        }
    }
}
