using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Validation;

namespace Portfolio.Application.Common.Validator;

public class ValidateID : IValidate<int>
{
    public ValidationResult Validate(int model)
    {
        var result = new ValidationResult();

        if (model <= 0)
            result.Errors.Add("Wrong ID, ID has to be greater then 0");

        return result;
    }
}
