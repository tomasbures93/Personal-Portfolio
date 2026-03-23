namespace Portfolio.Domain.Entities;

public sealed class WebsiteConfig
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public List<Technology> Technologies { get; set; }
}
