using UserService.Models.Domain.Enums;

namespace UserService.Models.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string FullName { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ProviderProfile? ProviderProfile { get; private set; }
    public CustomerProfile? CustomerProfile { get; private set; }
    
    private User() { }
    
    private User(Guid id, string email, string passwordHash, string fullName, UserRole role)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        FullName = fullName;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }
    
    public static User RegisterCustomer(Guid id, string email, string passwordHash, string fullName)
    {
        var user = new User(id, email, passwordHash, fullName, UserRole.Customer);
        user.CustomerProfile = new CustomerProfile(user.Id);
        return user;
    }
    public static User RegisterProvider(Guid id, string email, string passwordHash, string fullName, string businessName, string location)
    {
        var user = new User(id, email, passwordHash, fullName, UserRole.Provider);
        user.ProviderProfile = new ProviderProfile(user.Id, businessName, location);
        return user;
    }
}