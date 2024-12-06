using FluentValidation;

namespace Rira.Application.Users.Commands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(u => u.Id)
                .NotNull().WithMessage("Is is required");

            RuleFor(u => u.FirstName)
               .NotEmpty().WithMessage("this field is required")
               .MaximumLength(50).WithMessage("first name must be less than 50");

            RuleFor(u => u.LastName)
                   .NotEmpty().WithMessage("this field is required")
                   .MaximumLength(50).WithMessage("last name must be less than 50");

            RuleFor(u => u.BirthDate)
                .NotEmpty().WithMessage("this field is required");

            RuleFor(u => u.NationalCode)
                .NotEmpty().WithMessage("this field is required")
                .Length(10).WithMessage("this field's length must be 10 character");
        }
    }
}
