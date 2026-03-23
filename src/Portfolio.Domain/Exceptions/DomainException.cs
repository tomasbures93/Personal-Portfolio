namespace Portfolio.Domain.Exceptions;

public class DomainException : Exception
{
    public string ParamName { get; }
    public DomainException(string message, string paramName) : base(message) 
    { 
        ParamName = paramName;
    }
}
