using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public sealed class WebsiteConfig
{
    public int Id { get; init; }

    public string Email { get; private set; }

    public string UserName { get; private set; }

    public string PasswordHash { get; private set; }

    // TODO: There is better approach to handle this => IReadOnlyCollection to protect the collection from outside!!
    public ICollection<Technology>? Technologies { get; set; } = new List<Technology>();

    public WebsiteConfig() { }

    public void ChangeUserName(string userName)
    {
        Guard.AgainstNullOrWhiteSpace(userName, nameof(userName));
        UserName = userName;
    }

    public void ChangeEmail(string email)
    {
        Guard.AgainstNullOrWhiteSpace(email, nameof(email));
        Email = email;
    }

    public void UpdateTechnologies(List<Technology> technologies)
    {
        Guard.TechnologiesAreNotEmpty(technologies, nameof(technologies));
        Technologies.Clear();

        foreach(var technology in technologies)
        {
            Technologies.Add(technology);
        }
    }

    public void UpdatePasswordHash(string newHash)
    {
        Guard.AgainstNullOrWhiteSpace(newHash, nameof(newHash));
        PasswordHash = newHash;
    }
}
