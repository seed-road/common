using System.Collections;

namespace SeedRoad.Common.Core.Domain.Exceptions;

public class ExceptionsAggregate : Exception, IEnumerable<ISubstantiateException>, IDomainException
{
    public ExceptionsAggregate(IEnumerable<ISubstantiateException> exceptions)
    {
        Exceptions = exceptions;
    }

    public IEnumerable<ISubstantiateException> Exceptions { get; }

    public override string Message => "Multiple errors occured";

    public IEnumerator<ISubstantiateException> GetEnumerator()
    {
        return Exceptions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}