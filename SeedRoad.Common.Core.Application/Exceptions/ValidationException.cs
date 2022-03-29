using FluentValidation.Results;
using SeedRoad.Common.Core.Domain.Exceptions;

namespace SeedRoad.Common.Core.Application.Exceptions;

public class ValidationException : Exception, ISubstantiateException<IDictionary<string, string[]>>
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Reason = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Reason = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Reason { get; private set; }
}