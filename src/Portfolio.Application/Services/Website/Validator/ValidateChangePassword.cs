using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Validation;
using Portfolio.Application.DTO.Request;
using System.Text.RegularExpressions;

namespace Portfolio.Application.Services.Website.Validator;

public class ValidateChangePassword : IValidate<ChangePasswordRequestDto>
{
    public ValidationResult Validate(ChangePasswordRequestDto model)
    {
        var result = new ValidationResult();
        if (string.IsNullOrWhiteSpace(model.oldPassword))
            result.Errors.Add("Old Password is missing.");

        if (model.newPassword.Length < 10)
            result.Errors.Add("Password has to have atleast 10 Characters.");

        if (!Regex.IsMatch(model.newPassword, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[.,!?;:'\"()\\-_=+/@#$%&*])[A-Za-z\\d.,!?;:'\"()\\-_=+/@#$%&*]+$"))
            result.Errors.Add("Password has to have atleast 1 big letter (A-Z), 1 small letter (a-z) and 1 special character (.,!?;:'\"()-_=+/@#$%&*)");

        if(model.oldPassword == model.newPassword)
            result.Errors.Add("New password cannot be the same as old password.");

        return result;
    }
}
