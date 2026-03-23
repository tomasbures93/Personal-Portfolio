namespace Portfolio.Domain.Entities;

public sealed class Project
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Url { get; set; }

    public List<Technology> Technologies { get; set; }
}
