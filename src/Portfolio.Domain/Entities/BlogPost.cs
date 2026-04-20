using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public sealed class BlogPost
{
    public int Id { get; private set; }

    public string Title { get; private set; }

    public string Content { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public bool Draft { get; private set; }

    public string Creator { get; private set; }

    private BlogPost() { }

    public BlogPost(string title, string content, bool draft, string creator)
    {
        SetTitle(title);
        SetContent(content);
        Draft = draft;
        SetCreator(creator);
        CreatedAt = DateTime.UtcNow;
    }

    public BlogPost(int id, string title, string content, bool draft)
    {
        Guard.ValidId(id, nameof(id));
        Id = id;
        SetTitle(title);
        SetContent(content);
        Draft = draft;
    }

    public void Update(string title, string content, bool draft)
    {
        SetTitle(title);
        SetContent(content);
        Draft = draft;
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetTitle(string title)
    {
        Guard.AgainstNullOrWhiteSpace(title, nameof(title));
        Title = title.Trim();
    }

    private void SetContent(string content)
    {
        Guard.AgainstNullOrWhiteSpace(content, nameof(content));
        Content = content.Trim();
    }

    private void SetCreator(string creator)
    {
        Guard.AgainstNullOrWhiteSpace(creator, nameof(creator));
        Creator = creator.Trim();
    }
}
