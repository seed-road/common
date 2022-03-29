using System.Collections;

namespace SeedRoad.Common.Core.Domain.Exceptions;

public class ExceptionsAggregate : Exception, IEnumerable<Exception>, IDomainException
{
    public ExceptionsAggregate(IEnumerable<Exception> exceptions)
    {
        Exceptions = exceptions;
    }

    public IEnumerable<Exception> Exceptions { get; }

    public override string Message => "Multiple errors occured";

    public IEnumerator<Exception> GetEnumerator()
    {
        return Exceptions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}