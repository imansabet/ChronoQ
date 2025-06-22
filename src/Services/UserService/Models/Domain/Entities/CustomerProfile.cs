namespace UserService.Models.Domain.Entities;

public class CustomerProfile
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }

    private CustomerProfile() { }

    public CustomerProfile(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
    }
}