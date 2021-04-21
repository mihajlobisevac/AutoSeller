using Application.Common.Interfaces;
using Application.Common.Validation;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.V1.Brands.Commands
{
    public class EditBrandValidator : IValidationHandler<EditBrand.Command>
    {
        private readonly IApplicationDbContext _context;

        public EditBrandValidator(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> Validate(EditBrand.Command request)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Name == request.Name);

            if (brand is not null) return ValidationResult.Fail("Name already in use");

            return ValidationResult.Success;
        }
    }
}
