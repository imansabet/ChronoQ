using UserService.Models.Domain.Entities;

namespace UserService.Contracts;

public interface IUserRepository
{
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}