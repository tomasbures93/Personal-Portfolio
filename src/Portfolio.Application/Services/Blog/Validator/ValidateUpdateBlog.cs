using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Validation;
using Portfolio.Application.DTO.Request;

namespace Portfolio.Application.Services.Blog.Validator;

public class ValidateUpdateBlog : IValidate<BlogUpdateRequestDto>
{
    public ValidationResult Validate(BlogUpdateRequestDto model)
    {
        var result = new ValidationResult();

        if (model.Id <= 0)
            result.Errors.Add("Wrong ID, ID has to be greater then 0.");

        if (string.IsNullOrWhiteSpace(model.Title))
            result.Errors.Add("Title is missing.");

        if (string.IsNullOrWhiteSpace(model.Content))
            result.Errors.Add("Content is missing.");

        return result;
    }
}
