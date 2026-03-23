namespace Portfolio.Application.Common.Results;

public class Result
{
    public bool IsSuccess { get; protected set; }

    public ResultStatus Status { get; protected set; }

    public List<string> Errors { get; protected set; } = new();


    public Result() { }

    public Result(bool isSuccess, ResultStatus status, List<string>? errors = null)
    {
        IsSuccess = isSuccess;
        Status = status;
        Errors = errors ?? new List<string>();
    }

    public static Result Ok() => new Result(true, ResultStatus.Success);

    public static Result Failure(ResultStatus status, List<string> errors) => new Result(false, status, errors);

    public static Result Failure(ResultStatus status, string error) => new Result(false, status, new List<string> { error });
}
