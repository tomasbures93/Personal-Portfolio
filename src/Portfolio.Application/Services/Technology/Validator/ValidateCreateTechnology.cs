using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Validation;
using Portfolio.Application.DTO.Request;
using Portfolio.Domain.Enums;

namespace Portfolio.Application.Services.Technology.Validator;

public class ValidateCreateTechnology : IValidate<TechnologyRequestDto>
{
    public ValidationResult Validate(TechnologyRequestDto model)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(model.Name))
            result.Errors.Add("Technology Name is missing.");

        if (!Enum.IsDefined(typeof(TechnologyCategory), model.Category))
            result.Errors.Add("Wrong Technology category.");

        return result;
    }
}
