using MediatR;

namespace Rira.Application.Users.Queries.GetUser
{
    public record GetAllUserQuery() : IRequest<GetAllUsersResult>;
}