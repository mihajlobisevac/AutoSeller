﻿using Application.Common.Interfaces;
using Application.Common.Validation;
using Domain.Enums;
using System;
using System.Threading.Tasks;

namespace Application.V1.Posts.Commands
{
    public class CreatePostValidator : IValidationHandler<CreatePost.Command>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _date;

        public CreatePostValidator(IApplicationDbContext context, IDateTime date)
        {
            _context = context;
            _date = date;
        }

        public async Task<ValidationResult> Validate(CreatePost.Command request)
        {
            if (request.Year < 1900 || request.Year > _date.Now.Year + 1)
            {
                return ValidationResult.Fail($"Make sure year is between 1900 and {_date.Now.Year + 1}");
            }

            if (request.Mileage < 0) return ValidationResult.Fail($"Make sure mileage is greater than 0");

            if (!Enum.IsDefined(typeof(Drivetrain), request.Drivetrain)) return ValidationResult.Fail($"Invalid drivetrain");

            if (!Enum.IsDefined(typeof(Transmission), request.Transmission)) return ValidationResult.Fail($"Invalid transmission");

            if (!Enum.IsDefined(typeof(BodyStyle), request.Body)) return ValidationResult.Fail($"Invalid body style");

            var modelExists = await _context.Models.FindAsync(request.ModelId);
            if (modelExists is null)
            {
                return ValidationResult.Fail($"Model does not exist");
            }

            return ValidationResult.Success;
        }
    }
}
