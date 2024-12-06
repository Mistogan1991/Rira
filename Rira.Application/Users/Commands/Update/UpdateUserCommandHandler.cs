using MediatR;
using Microsoft.Extensions.Logging;
using Rira.Domain.Exceptions;
using Rira.Domain.Repositories;

namespace Rira.Application.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(IUserRepository repository, ILogger<UpdateUserCommandHandler> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("a UpdateUserCommand recived in {Handler} - command: {UpdateUserCommand}", this.GetType(), request.ToString());

            var user = await _repository.GetUserById(request.Id, cancellationToken);
            if (user == null)
            {
                _logger.LogError("user with id {Id} not found", request.Id);
                throw new EntityNotFoundException($"user with id {request.Id} not found");
            }

            try
            {
                user.Update(request.FirstName, request.LastName, request.NationalCode, request.BirthDate.Value);

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
