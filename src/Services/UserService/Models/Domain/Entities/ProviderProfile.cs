namespace UserService.Models.Domain.Entities;

public class ProviderProfile
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string BusinessName { get; private set; }
    public string Location { get; private set; }

    private ProviderProfile() { } //  EF

    public ProviderProfile(Guid userId, string businessName, string location)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        BusinessName = businessName;
        Location = location;
    }
}