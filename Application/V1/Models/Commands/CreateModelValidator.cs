using Application.Common.Interfaces;
using Application.Common.Validation;
using System.Threading.Tasks;

namespace Application.V1.Models.Commands
{
    public class CreateModelValidator : IValidationHandler<CreateModel.Command>
    {
        private readonly IApplicationDbContext _context;

        public CreateModelValidator(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> Validate(CreateModel.Command request)
        {
            var brand = await _context.Brands.FindAsync(request.BrandId);

            if (brand is null) return ValidationResult.Fail("Brand doesn't exist.");

            return ValidationResult.Success;
        }
    }
}
