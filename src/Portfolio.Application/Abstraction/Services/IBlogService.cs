using Portfolio.Application.Common.Results;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;

namespace Portfolio.Application.Abstraction.Services;

public interface IBlogService
{
    Task<Result<List<BlogPostResponseDto>>> GetBlogsAsync(CancellationToken token);

    Task<Result<BlogPostResponseDto>> GetBlogAsync(int blogId, CancellationToken token);

    Task<Result> DeleteBlogAsync(int blogId, CancellationToken token);

    Task<Result<BlogPostResponseDto>> CreateBlogAsync(BlogRequestDto blogRequestDto, string Creator, CancellationToken token);

    Task<Result<BlogPostResponseDto>> UpdateBlogAsync(BlogUpdateRequestDto blogUpdateRequestDto, CancellationToken token);
}
