using MediatR;
using Microsoft.Extensions.Logging;
using Rira.Domain.Entities;
using Rira.Domain.Repositories;

namespace Rira.Application.Users.Command.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        public CreateUserCommandHandler(IUserRepository userRepository, ILogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("a CreateUserCommand recived in {Handler} - command: {CreateUserCommand}", this.GetType(), request.ToString());
            
            try
            {
                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    NationalCode = request.NationalCode,
                    BirthDate = request.BirthDate.Value
                };

                _userRepository.Add(user);
                await _userRepository.SaveAsync(cancellationToken);

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
