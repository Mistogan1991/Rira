using MediatR;
using Microsoft.Extensions.Logging;
using Rira.Domain.Exceptions;
using Rira.Domain.Repositories;

namespace Rira.Application.Users.Commands.Update
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUserRepository repository, ILogger<DeleteUserCommandHandler> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }

        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("a DeleteUserCommand recived in {Handler} - command: {DeleteUserCommand}", this.GetType(), request.ToString());

            var user = await _repository.GetUserById(request.Id, cancellationToken);
            if (user == null)
            {
                _logger.LogError("user with id {Id} not found", request.Id);
                throw new EntityNotFoundException($"user with id {request.Id} not found");
            }

            try
            {
                user.Delete();

                _repository.Update(user);
                await _repository.SaveAsync(cancellationToken);

                return user.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("an exception have occurred in {Handler} with message : {Message}", this.GetType(), ex.Message);
                throw;
            }
        }
    }
}
