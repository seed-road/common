using MediatR;
using SeedRoad.Common.Core.Domain.Exceptions;

namespace SeedRoad.Common.Core.Application.Notifications;

public record ExceptionNotification<TException>(TException Exception) : INotification
    where TException : IDomainException
{
    public TException Exception { get; } = Exception;
}