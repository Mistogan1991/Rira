using MediatR;
using Rira.Domain.Repositories;

namespace Rira.Application.Users.Queries.GetUser
{
    public class GetAllUserQueryHandler(IUserRepository repository) : IRequestHandler<GetAllUserQuery, GetAllUsersResult>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<GetAllUsersResult> Handle(GetAllUserQuery request, CancellationToken cancellationToken = default)
        {
            var users = await _repository.GetAllUsers(cancellationToken);
            var result = users.Select(u => new GetUserDto(u.Id, u.FirstName, u.LastName, u.NationalCode, u.BirthDate)).ToList();

            return new GetAllUsersResult
            {
                Users = result
            };
        }
    }
}