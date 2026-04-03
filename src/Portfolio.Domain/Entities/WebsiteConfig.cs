using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public sealed class WebsiteConfig
{
    public int Id { get; init; }

    public string Email { get; set; }

    public string UserName { get; set; }

    public string PasswordHash { get; set; }

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
        PasswordHash = newHash;
    }
}
