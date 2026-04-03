using Portfolio.Domain.Entities;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Common;

public static class Guard
{
    public static void AgainstNullOrWhiteSpace(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Value cannot be null or whitespace.", paramName);
    }

    public static void EnumValueExists<TEnum>(TEnum value, string paramName) where TEnum : Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value))
            throw new DomainException("Invalid enum value.", paramName);
    }

    public static void ValidId(int value, string paramName)
    {
        if (value <= 0)
            throw new DomainException("ID cannot be null or less", paramName);
    }

    public static void TechnologiesAreNotEmpty(List<Technology> technologies, string paramName)
    {
        if (technologies.Count == 0)
            throw new DomainException("Technologies can´t be empty", paramName);
    }
}
