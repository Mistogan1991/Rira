using Rira.Domain.Entities;

namespace Rira.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken);
        Task<User> GetUserById(int userId, CancellationToken cancellationToken);
        void Add(User user);
        void Update(User user);
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
