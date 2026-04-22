using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Validation;
using Portfolio.Application.DTO.Request;

namespace Portfolio.Application.Services.Project.Validator;

public class ValidateCreateProject : IValidate<ProjectRequestDto>
{
    public ValidationResult Validate(ProjectRequestDto model)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(model.Title))
            result.Errors.Add("Title is missing.");

        if (string.IsNullOrWhiteSpace(model.Description))
            result.Errors.Add("Descriptions are missing.");

        if (!model.Technologies.Any())
            result.Errors.Add("Technologies are missing.");

        return result;
    }
}
