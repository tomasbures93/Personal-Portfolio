using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public sealed class WebsiteConfig
{
    public int Id { get; private set; }

    public string Email { get; private set; }

    public string UserName { get; private set; }

    public string PasswordHash { get; private set; }


    private readonly List<Technology> _technologies = new();

    public IReadOnlyCollection<Technology> Technologies => _technologies.AsReadOnly();

    private WebsiteConfig() { }

    public WebsiteConfig(string userName, string email)
    {
        ChangeUserName(userName);
        ChangeEmail(email);
    }

    public void UpdateProfil(string email, IEnumerable<Technology> technologies)
    {
        ChangeEmail(email);
        UpdateTechnologies(technologies);
    }

    public void ChangeUserName(string userName)
    {
        Guard.AgainstNullOrWhiteSpace(userName, nameof(userName));
        UserName = userName.Trim();
    }

    public void ChangeEmail(string email)
    {
        Guard.AgainstNullOrWhiteSpace(email, nameof(email));
        Email = email.Trim();
    }

    private void UpdateTechnologies(IEnumerable<Technology> technologies)
    {
        var items = technologies.ToList();
        Guard.TechnologiesAreNotEmpty(items, nameof(technologies));
        _technologies.Clear();
        _technologies.AddRange(technologies);
    }

    public void UpdatePasswordHash(string newHash)
    {
        Guard.AgainstNullOrWhiteSpace(newHash, nameof(newHash));
        PasswordHash = newHash;
    }
}
