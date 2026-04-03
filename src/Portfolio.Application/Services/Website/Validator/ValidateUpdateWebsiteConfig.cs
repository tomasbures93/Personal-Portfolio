using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Validation;
using Portfolio.Application.DTO.Request;

namespace Portfolio.Application.Services.Website.Validator;

public class ValidateUpdateWebsiteConfig : IValidate<WebsiteConfigUpdateRequestDto>
{
    public ValidationResult Validate(WebsiteConfigUpdateRequestDto model)
    {
        var result = new ValidationResult();

        return result;
    }
}
