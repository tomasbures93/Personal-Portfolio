using Portfolio.Domain.Common;
using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public class Technology
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public TechnologyCategory Category { get; private set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();

    public int? WebsiteConfigId { get; set; }

    public WebsiteConfig? WebsiteConfig { get; set; }

    protected Technology() { }

    public Technology(string name, TechnologyCategory category)
    {
        UpdateName(name);
        UpdateCategory(category);
    }

    public Technology(int id, string name, TechnologyCategory category)
    {
        Guard.ValidId(id, nameof(id));
        Id = id;
        UpdateName(name);
        UpdateCategory(category);
    }

    public void UpdateName(string name)
    {
        Guard.AgainstNullOrWhiteSpace(name, nameof(name));
        Name = name.Trim();
    }

    public void UpdateCategory(TechnologyCategory category) 
    {
        Guard.EnumValueExists(category, nameof(category));
        Category = category;
    }
}
