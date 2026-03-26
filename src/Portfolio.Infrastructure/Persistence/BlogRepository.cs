using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Abstraction.Persistence;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence;

public sealed class BlogRepository : IBlogRepository
{
    private readonly AppDbContext _dbContext;

    public BlogRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BlogPost> CreateBlogAsync(BlogPost blogPost, CancellationToken token)
    {
        await _dbContext.BlogPosts.AddAsync(blogPost, token);
        await _dbContext.SaveChangesAsync(token);

        return blogPost;
    }

    public async Task<bool> DeleteBlogAsync(int blogId, CancellationToken token)
    {
        var blog = await _dbContext.BlogPosts.SingleOrDefaultAsync(b => b.Id == blogId, token);
        if (blog == null)
            return false;

        _dbContext.BlogPosts.Remove(blog);
        await _dbContext.SaveChangesAsync(token);

        return true;
    }

    public async Task<BlogPost?> GetBlogAsync(int blogId, CancellationToken token)
    {
        return await _dbContext.BlogPosts.SingleOrDefaultAsync(b => b.Id == blogId, token);
    }

    public async Task<List<BlogPost>> GetBlogsAsync(CancellationToken token)
    {
        return await _dbContext.BlogPosts.ToListAsync(token);
    }

    public async Task<BlogPost?> UpdateBlogAsync(BlogPost blogPost, CancellationToken token)
    {
        var existingBlog = await _dbContext.BlogPosts.SingleOrDefaultAsync(b => b.Id == blogPost.Id, token);
        if (existingBlog == null)
            return null;

        existingBlog.UpdateTitle(blogPost.Title);
        existingBlog.UpdateContent(blogPost.Content);
        existingBlog.SetUpdatedAt();
        existingBlog.UpdateDraft(blogPost.Draft);

        await _dbContext.SaveChangesAsync(token);

        return existingBlog;
    }
}
