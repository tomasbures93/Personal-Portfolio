using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public sealed class Project
{
    public int Id { get; private set; }

    public string Title { get; private set; }
     
    public string Description { get; private set; }

    public string? Url { get; private set; }


    private readonly List<Technology> _technologies = new();

    public IReadOnlyCollection<Technology> Technologies => _technologies.AsReadOnly();

    private Project() { }

    public Project(string title, string description, List<Technology> technologies, string? url = null)
    {
        UpdateTitle(title);
        UpdateDescription(description);
        UpdateTechnologies(technologies);
        UpdateUrl(url);
    }

    public Project(int id, string title, string description, List<Technology> technologies, string? url = null)
    {
        Guard.ValidId(id, nameof(id));
        Id = id;
        UpdateTitle(title);
        UpdateDescription(description);
        UpdateTechnologies(technologies);
        UpdateUrl(url);
    }

    public void Update(string title, string description, List<Technology> technologies, string? url = null)
    {
        UpdateTitle(title);
        UpdateDescription(description);
        UpdateTechnologies(technologies);
        UpdateUrl(url);
    }

    private void UpdateTitle(string title)
    {
        Guard.AgainstNullOrWhiteSpace(title, nameof(title));
        Title = title.Trim();
    }

    private void UpdateDescription(string description)
    {
        Guard.AgainstNullOrWhiteSpace(description, nameof(description));
        Description = description.Trim();
    }

    private void UpdateTechnologies(IEnumerable<Technology> technologies)
    {
        var items = technologies.ToList();
        Guard.TechnologiesAreNotEmpty(items, nameof(technologies));
        _technologies.Clear();
        _technologies.AddRange(technologies);
    }

    private void UpdateUrl(string? url)
    {
        Url = string.IsNullOrWhiteSpace(url) ? null : url.Trim();   
    }
}
