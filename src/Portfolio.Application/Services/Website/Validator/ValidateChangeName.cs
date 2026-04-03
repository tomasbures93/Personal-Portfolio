using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Validation;
using Portfolio.Application.DTO.Request;
using System.Text.RegularExpressions;

namespace Portfolio.Application.Services.Website.Validator;

public class ValidateChangeName : IValidate<ChangeNameRequestDto>
{
    public ValidationResult Validate(ChangeNameRequestDto model)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(model.name))
            result.Errors.Add("Name is missing!");

        if (model.name.Length <= 8)
            result.Errors.Add("Name has to be atleast 8 characters Long!");

        if (!Regex.IsMatch(model.name, "^[a-zA-Z0-9]+$"))
            result.Errors.Add("You can´t use special characters in your name!");

        return result;
    }
}
