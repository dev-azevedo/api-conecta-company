using System.Net;

namespace ConectaCompany.Application.Response;

public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public HttpStatusCode StatusCode { get; }

    protected Result(bool isSuccess, string? error, HttpStatusCode statusCode)
    {
        IsSuccess = isSuccess;
        Error = error;
        StatusCode = statusCode;
    }

    public static Result Success(HttpStatusCode statusCode = HttpStatusCode.OK)
        => new(true, null, statusCode);

    public static Result Failure(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new(false, error, statusCode);
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool isSuccess, T? value, string? error, HttpStatusCode statusCode)
        : base(isSuccess, error, statusCode)
    {
        Value = value;
    }

    public static Result<T> Success(T value, HttpStatusCode statusCode = HttpStatusCode.OK)
        => new(true, value, null, statusCode);

    public static new Result<T> Failure(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new(false, default, error, statusCode);
}