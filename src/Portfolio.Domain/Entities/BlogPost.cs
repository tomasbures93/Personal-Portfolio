using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public sealed class BlogPost
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool Draft { get; set; }

    public string Creator { get; set; }

    public BlogPost() { }

    public BlogPost(string title, string content, bool draft, string creator)
    {
        UpdateTitle(title);
        UpdateContent(content);
        UpdateDraft(draft);
        UpdateCreator(creator);
        CreatedAt = DateTime.UtcNow;
    }

    public BlogPost(int id, string title, string content, bool draft)
    {
        Guard.ValidId(id, nameof(id));
        Id = id;
        UpdateTitle(title);
        UpdateContent(content);
        UpdateDraft(draft);
    }

    public void UpdateTitle(string title)
    {
        Guard.AgainstNullOrWhiteSpace(title, nameof(title));
        Title = title.Trim();
    }

    public void UpdateContent(string content)
    {
        Guard.AgainstNullOrWhiteSpace(content, nameof(content));
        Content = content.Trim();
    }

    public void UpdateCreator(string creator)
    {
        Guard.AgainstNullOrWhiteSpace(creator, nameof(creator));
        Creator = creator;
    }

    public void UpdateDraft(bool draft)
    {
        Draft = draft;
    }

    public void SetUpdatedAt()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
