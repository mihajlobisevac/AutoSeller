using Application.Common.Interfaces;
using Application.Common.Validation;
using Application.V1.Posts.Commands;
using System.Threading.Tasks;

namespace Application.V1.Posts.Validators
{
    public class ToggleRecallPostValidator : IValidationHandler<ToggleRecallPost.Command>
    {
        private readonly IApplicationDbContext _context;

        public ToggleRecallPostValidator(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> Validate(ToggleRecallPost.Command request)
        {
            var post = await _context.Posts.FindAsync(request.PostId);
            if (post is null)
            {
                return ValidationResult.Fail($"Post does not exist");
            }

            return ValidationResult.Success;
        }
    }
}
