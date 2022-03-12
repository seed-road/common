using System.Collections;

namespace SeedRoad.Common.Domain.Exceptions;

public class ExceptionsAggregate : Exception, IEnumerable<ISubstantiateException>, IApplicationException
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