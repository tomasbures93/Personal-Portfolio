using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Validation;
using Portfolio.Application.DTO.Request;
using Portfolio.Domain.Enums;

namespace Portfolio.Application.Services.Technology.Validator;

public class ValidateUpdateTechnology : IValidate<TechnologyUpdateRequestDto>
{
    public ValidationResult Validate(TechnologyUpdateRequestDto model)
    {
        var result = new ValidationResult();

        if (model.id <= 0)
            result.Errors.Add("Wrong ID, ID has to be greater then 0.");

        if (string.IsNullOrWhiteSpace(model.name))
            result.Errors.Add("Technology Name is missing.");

        if (!Enum.IsDefined(typeof(TechnologyCategory), model.category))
            result.Errors.Add("Wrong Technology category.");

        return result;
    }
}
