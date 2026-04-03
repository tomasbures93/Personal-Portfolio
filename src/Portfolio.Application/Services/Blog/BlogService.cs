using Portfolio.Application.Abstraction.Persistence;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Results;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services.Blog;

public sealed class BlogService : IBlogService
{
    private readonly IBlogRepository _repository;
    private readonly IValidate<int> _validateID;
    private readonly IValidate<BlogRequestDto> _validateBlogCreateRequest;
    private readonly IValidate<BlogUpdateRequestDto> _validateBlogUpdateRequest;

    public BlogService(
        IBlogRepository repository, 
        IValidate<int> validateID,
        IValidate<BlogRequestDto> validateBlogCreateRequest,
        IValidate<BlogUpdateRequestDto> validateBlogUpdateRequest)
    {
        _repository = repository;
        _validateID = validateID;
        _validateBlogCreateRequest = validateBlogCreateRequest;
        _validateBlogUpdateRequest = validateBlogUpdateRequest;
    }

    // TODO: Add Creator from Auth
    public async Task<Result<BlogPostResponseDto>> CreateBlogAsync(BlogRequestDto blogRequestDto, CancellationToken token)
    {
        var validationResult = _validateBlogCreateRequest.Validate(blogRequestDto);
        if (!validationResult.IsValid)
            return Result<BlogPostResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var blogModel = new BlogPost(blogRequestDto.title, blogRequestDto.content, blogRequestDto.draft, "Default");
        var blog = await _repository.CreateBlogAsync(blogModel, token);

        var blogDto = new BlogPostResponseDto(
            blog.Id, 
            blog.Title, 
            blog.Content, 
            blog.CreatedAt, 
            blog.UpdatedAt, 
            blog.Draft, 
            blog.Creator);
        return Result<BlogPostResponseDto>.Ok(blogDto);
    }

    public async Task<Result> DeleteBlogAsync(int blogId, CancellationToken token)
    {
        var validationResult = _validateID.Validate(blogId);
        if (!validationResult.IsValid)
            return Result.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var result = await _repository.DeleteBlogAsync(blogId, token);
        if (!result)
            return Result.Failure(ResultStatus.NotFound, "BlogPost not found");

        return Result.Ok();
    }

    public async Task<Result<BlogPostResponseDto>> GetBlogAsync(int blogId, CancellationToken token)
    {
        var validationResult = _validateID.Validate(blogId);
        if (!validationResult.IsValid)
            return Result<BlogPostResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var blog = await _repository.GetBlogAsync(blogId, token);
        if (blog == null)
            return Result<BlogPostResponseDto>.Failure(ResultStatus.NotFound, "Blog not found");

        var blogDto = new BlogPostResponseDto(
            blog.Id, 
            blog.Title, 
            blog.Content, 
            blog.CreatedAt, 
            blog.UpdatedAt, 
            blog.Draft, 
            blog.Creator);
        return Result<BlogPostResponseDto>.Ok(blogDto);
    }

    public async Task<Result<List<BlogPostResponseDto>>> GetBlogsAsync(CancellationToken token)
    {
        var blogs = await _repository.GetBlogsAsync(token);

        var blogsDto = blogs
            .Select(b => new BlogPostResponseDto(
                b.Id, 
                b.Title, 
                b.Content, 
                b.CreatedAt, 
                b.UpdatedAt, 
                b.Draft, 
                b.Creator))
            .OrderBy(d => d.id)
            .ToList();
        return Result<List<BlogPostResponseDto>>.Ok(blogsDto);
    }

    public async Task<Result<BlogPostResponseDto>> UpdateBlogAsync(BlogUpdateRequestDto blogUpdateRequestDto, CancellationToken token)
    {
        var validationResult = _validateBlogUpdateRequest.Validate(blogUpdateRequestDto);
        if (!validationResult.IsValid)
            return Result<BlogPostResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var blogModel = new BlogPost(
            blogUpdateRequestDto.id,
            blogUpdateRequestDto.title,
            blogUpdateRequestDto.content,
            blogUpdateRequestDto.draft);
        var blog = await _repository.UpdateBlogAsync(blogModel, token);
        if (blog == null)
            return Result<BlogPostResponseDto>.Failure(ResultStatus.NotFound, "Blog was not found.");

        var blogDto = new BlogPostResponseDto(
            blog.Id,
            blog.Title,
            blog.Content,
            blog.CreatedAt,
            blog.UpdatedAt,
            blog.Draft,
            blog.Creator);
        
        return Result<BlogPostResponseDto>.Ok(blogDto);
    }
}
