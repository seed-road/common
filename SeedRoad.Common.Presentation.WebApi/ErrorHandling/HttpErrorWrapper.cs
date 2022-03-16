namespace SeedRoad.Common.Presentation.WebApi.ErrorHandling;

public record HttpErrorWrapper(int HttpCode, HttpError HttpError);