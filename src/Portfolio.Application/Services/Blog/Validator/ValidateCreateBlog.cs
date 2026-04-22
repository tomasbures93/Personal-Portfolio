using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Validation;
using Portfolio.Application.DTO.Request;

namespace Portfolio.Application.Services.Blog.Validator;

public class ValidateCreateBlog : IValidate<BlogRequestDto>
{
    public ValidationResult Validate(BlogRequestDto model)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(model.Title))
            result.Errors.Add("Title is missing.");

        if (string.IsNullOrWhiteSpace(model.Content))
            result.Errors.Add("Content is missing.");

        return result;
    }
}
