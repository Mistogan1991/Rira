using MediatR;
using Rira.Domain.Exceptions;
using Rira.Domain.Repositories;

namespace Rira.Application.Users.Queries.GetUser;

public class GetUserQueryHandler(IUserRepository repository) : IRequestHandler<GetUserQuery, GetUserDto>
{
    private readonly IUserRepository _repository = repository;

    public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetUserById(request.Id,cancellationToken);

        if (user == null)
        {
            throw new EntityNotFoundException($"user with id: {request.Id} not exist");
        }

        var userDto = new GetUserDto(user.Id, user.FirstName, user.LastName, user.NationalCode, user.BirthDate);

        return userDto;
    }
}