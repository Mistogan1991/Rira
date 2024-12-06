using MediatR;

namespace Rira.Application.Users.Commands.Update
{
    public record DeleteUserCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
