namespace SeedRoad.Common.Core.Application.Validation;

public interface ICommandValidator<TCommand>
{
    public Task Validate(TCommand command);
}