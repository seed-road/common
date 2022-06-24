using System.Collections;

namespace SeedRoad.Common.Core.Domain.Exceptions;

public class ExceptionsAggregate : Exception, IExceptionsAggregate
{
    public ExceptionsAggregate()
    {
        Exceptions = new List<Exception>();
    }

    public ExceptionsAggregate(IEnumerable<Exception> exceptions)
    {
        Exceptions = exceptions.ToList();
    }

    public IList<Exception> Exceptions { get; }

    public override string Message => "Multiple errors occured";

    public IEnumerator<Exception> GetEnumerator()
    {
        return Exceptions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void ThrowIfAny()
    {
        if (Exceptions.Any()) throw this;
    }

    public Exception AggregatedException => this;

    public void Add(Exception item)
    {
        Exceptions.Add(item);
    }

    public void Clear()
    {
        Exceptions.Clear();
    }

    public bool Contains(Exception item)
    {
        return Exceptions.Contains(item);
    }

    public void CopyTo(Exception[] array, int arrayIndex)
    {
        Exceptions.CopyTo(array, arrayIndex);
    }

    public bool Remove(Exception item)
    {
        return Exceptions.Remove(item);
    }

    public int Count => Exceptions.Count;

    public bool IsReadOnly => Exceptions.IsReadOnly;

    public int IndexOf(Exception item)
    {
        return Exceptions.IndexOf(item);
    }

    public void Insert(int index, Exception item)
    {
        Exceptions.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        Exceptions.RemoveAt(index);
    }

    public Exception this[int index]
    {
        get => Exceptions[index];
        set => Exceptions[index] = value;
    }


    public override string ToString()
    {
        return string.Join("\n", Exceptions.Select(e => e.ToString()));
    }
}