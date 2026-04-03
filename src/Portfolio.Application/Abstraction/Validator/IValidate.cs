using Portfolio.Application.Common.Validation;

namespace Portfolio.Application.Abstraction.Validator;

public interface IValidate<T>
{
    ValidationResult Validate(T model);
}
