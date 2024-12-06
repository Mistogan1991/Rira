using FluentValidation;

namespace Rira.Application.Users.Commands.Update
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(u => u.Id)
                .NotNull().WithMessage("Is is required");
        }
    }
}
