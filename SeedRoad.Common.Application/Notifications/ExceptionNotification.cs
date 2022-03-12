using MediatR;
using SeedRoad.Common.Domain.Exceptions;

namespace SeedRoad.Common.Application.Notifications;

public record ExceptionNotification<TException>(TException Exception) : INotification
    where TException : IApplicationException
{
    public TException Exception { get; } = Exception;
}