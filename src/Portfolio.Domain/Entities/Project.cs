using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public sealed class Project
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string? Url { get; set; }

    public ICollection<Technology> Technologies { get; set; } = new List<Technology>();

    public Project() { }

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

    public void UpdateTitle(string title)
    {
        Guard.AgainstNullOrWhiteSpace(title, nameof(title));
        Title = title.Trim();
    }

    public void UpdateDescription(string description)
    {
        Guard.AgainstNullOrWhiteSpace(description, nameof(description));
        Description = description.Trim();
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

    public void UpdateUrl(string? url)
    {
        if (url == null) return;
        Url = url.Trim();
    }
}
