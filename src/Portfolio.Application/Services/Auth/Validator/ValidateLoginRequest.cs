using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Validation;
using Portfolio.Application.DTO.Request;

namespace Portfolio.Application.Services.Auth.Validator;

public class ValidateLoginRequest : IValidate<LoginRequestDto>
{
    public ValidationResult Validate(LoginRequestDto model)
    {
        var result = new ValidationResult();

        if(string.IsNullOrWhiteSpace(model.Login))
        {
            result.Errors.Add("Login is required.");
        }

        if(string.IsNullOrWhiteSpace(model.Password))
        {
            result.Errors.Add("Password is required.");
        }

        return result;
    }
}
