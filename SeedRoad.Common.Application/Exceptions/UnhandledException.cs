using System.Text;
using SeedRoad.Common.Domain.Exceptions;

namespace SeedRoad.Common.Application.Exceptions;

public class UnhandledException : Exception, ISubstantiateException
{
    public const string DefaultMessage = "An unexpected technical error occured";

    public UnhandledException(Exception innerException) : base(DefaultMessage, innerException)
    {
    }

    public override string StackTrace
    {
        get
        {
            var stringBuilder = new StringBuilder();
            Exception exception = InnerException;
            while (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);
                exception = exception.InnerException;
            }

            return stringBuilder.ToString();
        }
    }

    public string Reason => InnerException?.Message ?? "No more details";
}