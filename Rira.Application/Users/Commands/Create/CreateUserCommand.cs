using MediatR;

namespace Rira.Application.Users.Command.Create
{
    public record CreateUserCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
