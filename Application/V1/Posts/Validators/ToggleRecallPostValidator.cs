using Application.Common.Interfaces;
using Application.Common.Validation;
using Application.V1.Posts.Commands;
using System.Threading.Tasks;

namespace Application.V1.Posts.Validators
{
    public class ToggleRecallPostValidator : IValidationHandler<ToggleRecallPost.Command>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public ToggleRecallPostValidator(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<ValidationResult> Validate(ToggleRecallPost.Command request)
        {
            var post = await _context.Posts.FindAsync(request.PostId);
            if (post is null)
            {
                return ValidationResult.Fail("Post does not exist");
            }

            if (post.CreatedBy != _currentUser.UserId && _currentUser.IsAdmin == false)
            {
                return ValidationResult.Fail("Unauthorized to toggle recalled state of this post");
            }

            return ValidationResult.Success;
        }
    }
}
