using Microsoft.EntityFrameworkCore;
using Rira.Domain.Entities;
using Rira.Domain.Repositories;

namespace Rira.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            this._context = context;             
        }


        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public async Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking().Where(u => !u.IsDeleted).ToListAsync(cancellationToken);
        }

        public async Task<User> GetUserById(int userId, CancellationToken cancellationToken)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == userId && !u.IsDeleted, cancellationToken);
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result;
        }

        public async void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
