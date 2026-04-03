namespace Portfolio.Application.Common.Results;

public class Result<T> : Result
{
    public T Value { get; private set; }

    private Result(T value, bool isSuccess, ResultStatus status, List<string>? errors = null) : base(isSuccess, status, errors)
    {
        Value = value;
    }

    public static Result<T> Ok(T value) => new Result<T>(value, true, ResultStatus.Success);

    public static new Result<T> Failure(ResultStatus status, List<string> errors) => new Result<T>(default, false, status, errors);

    public static new Result<T> Failure(ResultStatus status, string error) => new Result<T>(default, false, status, new List<string> { error });   
}
