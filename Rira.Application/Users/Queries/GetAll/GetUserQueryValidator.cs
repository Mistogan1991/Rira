using FluentValidation;
using Rira.Application.Users.Queries.GetUser;

namespace Rira.Application.Users.Queries.GetAll
{
    public class GetUserQueryValidator : AbstractValidator<GetAllUserQuery>
    {
        public GetUserQueryValidator()
        {}
    }
}