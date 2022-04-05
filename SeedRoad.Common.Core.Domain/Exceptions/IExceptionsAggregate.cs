namespace SeedRoad.Common.Core.Domain.Exceptions;

public interface IExceptionsAggregate: IList<Exception>, IDomainException
{
    void ThrowIfAny();
    
    Exception AggregatedException { get; }
}