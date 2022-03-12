namespace SeedRoad.Common.WebApi.ErrorHandling;

public record HttpErrorWrapper(int HttpCode, HttpError HttpError);