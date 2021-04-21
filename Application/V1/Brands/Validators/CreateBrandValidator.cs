using Application.Common.Interfaces;
using Application.Common.Validation;
using Application.V1.Brands.Commands;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.V1.Brands.Validators
{
    public class CreateBrandValidator : IValidationHandler<CreateBrand.Command>
    {
        private readonly IApplicationDbContext _context;

        public CreateBrandValidator(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> Validate(CreateBrand.Command request)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Name == request.Name);

            if (brand is not null) return ValidationResult.Fail("Name already in use");

            return ValidationResult.Success;
        }
    }
}
