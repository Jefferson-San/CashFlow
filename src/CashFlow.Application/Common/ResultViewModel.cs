using Flunt.Notifications;

namespace CashFlow.Application.Common;
public class ResultViewModel<T>
{
    public bool IsSuccess { get; }
    public T? Data { get; }
    public Error? Error { get; }

    public ResultViewModel(T data)
    {
        IsSuccess = true;
        Data = data;
    }

    private ResultViewModel(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static ResultViewModel<T> Success(T data) => new (data);
    public static ResultViewModel<T> Failure(Error error) => new (error);
}
