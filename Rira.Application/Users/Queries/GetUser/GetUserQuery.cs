using MediatR;

namespace Rira.Application.Users.Queries.GetUser;

public record GetUserQuery(int Id) : IRequest<GetUserDto>;