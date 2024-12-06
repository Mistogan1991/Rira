using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Rira.Application.Users.Command.Create;
using Rira.Application.Users.Commands.Update;
using Rira.Application.Users.Queries.GetUser;

namespace Rira.Presentation.Services
{

    public class UserServiceImpl : UserService.UserServiceBase
    {
        private readonly IMediator _mediator;

        public UserServiceImpl(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CommandResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var command = new CreateUserCommand
            {
                FirstName = request.FirstName ?? string.Empty,
                LastName = request.LastName ?? string.Empty,
                BirthDate = request.BirthDate?.ToDateTime().ToUniversalTime(),
                NationalCode = request.NationalCode ?? string.Empty,
            };

            var id = await _mediator.Send(command);

            return new CommandResponse
            {
                Success = true,
                Message = $"User with id: {id} created successfully"
            };
        }

        public override async Task<UserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var user = await _mediator.Send(new GetUserQuery(request.Id));

            return new UserResponse
            {
                User = new User
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    NationalCode = user.NationalCode,
                    BirthDate = user.BirthDate.ToTimestamp()
                }
            };
        }

        public override async Task<CommandResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            var updateCommand = new UpdateUserCommand
            {
                Id = request.Id,
                FirstName = request.FirstName ?? string.Empty,
                LastName = request.LastName ?? string.Empty,
                NationalCode = request.NationalCode ?? string.Empty,
                BirthDate = request.BirthDate?.ToDateTime().ToUniversalTime(),
            };

            var id = await _mediator.Send(updateCommand);

            return new CommandResponse
            {
                Success = true,
                Message = $"User with id: {id} updated successfully"
            };
        }

        public override async Task<CommandResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            var command = new DeleteUserCommand
            {
                Id = request.Id,
            };

            await _mediator.Send(command);

            return new CommandResponse
            {
                Success = true,
                Message = $"User with id: {request.Id} deleted successfully"
            };
        }

        public override async Task<ListUsersResponse> ListUsers(ListUsersRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetAllUserQuery());

            var response = new ListUsersResponse();
            response.Users.AddRange(result.Users.Select(u => new User
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                NationalCode = u.NationalCode,
                BirthDate = u.BirthDate.ToTimestamp(),
            }));

            return response;
        }
    }
}
