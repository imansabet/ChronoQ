using Microsoft.EntityFrameworkCore;
using UserService.Contracts;
using UserService.Models.Domain.Entities;

namespace UserService.Persistence;

public class UserRepository(UserDbContext dbContext) : IUserRepository
{
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await dbContext.Users
            .Include(u =>u.ProviderProfile)
            .Include(u =>u.CustomerProfile)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task AddAsync(User user)
    {
         await dbContext.Users.AddAsync(user);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}