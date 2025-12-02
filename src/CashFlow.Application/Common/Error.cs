using Flunt.Notifications;
using System.Net;

namespace CashFlow.Application.Common;

public record Error
{
    public static Error None => new((int)HttpStatusCode.InternalServerError, string.Empty, ErrorTypeEnum.Failure, null);
    public int StatusCode { get; }
    public string Message { get; }
    public ErrorTypeEnum Type { get; }
    public IReadOnlyCollection<Notification>? Notifications { get; }
    public Error(int statusCode, string message, ErrorTypeEnum type, IReadOnlyCollection<Notification>? notifications = null)
    {
        StatusCode = statusCode;
        Message = message;
        Type = type;
        Notifications = notifications;
    }
    public static Error Failure(string message, IReadOnlyCollection<Notification>? notifications) =>
        new((int)HttpStatusCode.InternalServerError, message, ErrorTypeEnum.Failure, notifications);
    public static Error Validation(string message, IReadOnlyCollection<Notification>? notifications) =>
        new((int)HttpStatusCode.BadRequest, message, ErrorTypeEnum.Validation, notifications);
    public static Error NotFound(string message, IReadOnlyCollection<Notification>? notifications) =>
        new((int)HttpStatusCode.NotFound, message, ErrorTypeEnum.NotFound, notifications);
    public static Error Conflict(string message, IReadOnlyCollection<Notification>? notifications) =>
        new((int)HttpStatusCode.Conflict, message, ErrorTypeEnum.Conflict, notifications);
    public static Error Unathorized(string message, IReadOnlyCollection<Notification>? notifications) =>
        new((int)HttpStatusCode.Unauthorized, message, ErrorTypeEnum.Unauthorized , notifications);
} 
