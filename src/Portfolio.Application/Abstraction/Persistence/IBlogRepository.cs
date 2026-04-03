using Portfolio.Domain.Entities;

namespace Portfolio.Application.Abstraction.Persistence;

public interface IBlogRepository
{
    Task<List<BlogPost>> GetBlogsAsync(CancellationToken token);

    Task<BlogPost?> GetBlogAsync(int blogId, CancellationToken token);

    Task<bool> DeleteBlogAsync(int blogId, CancellationToken token);

    Task<BlogPost> CreateBlogAsync(BlogPost blogPost, CancellationToken token);

    Task<BlogPost?> UpdateBlogAsync(BlogPost blogPost, CancellationToken token);
}
