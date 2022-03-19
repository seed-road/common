using MediatR;

namespace SeedRoad.Common.Core.Application.Pagination;

public class PaginationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly PaginationConfiguration _paginationConfiguration;

    public PaginationBehavior(PaginationConfiguration paginationConfiguration)
    {
        _paginationConfiguration = paginationConfiguration;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (request is PaginationQueryBase<TResponse> paginationQuery)
        {
            UpdatePaginationQueryIfNeeded(paginationQuery);
        }

        return await next();
    }

    private void UpdatePaginationQueryIfNeeded(PaginationQueryBase<TResponse> paginationQuery)
    {
        if (paginationQuery.Page > _paginationConfiguration.DefaultMaxSize ||
            paginationQuery.Page is IPagination.UnsetPaginationValue)
        {
            paginationQuery.Page = _paginationConfiguration.DefaultPage;
        }

        if (paginationQuery.Size is IPagination.UnsetPaginationValue or < 0)
        {
            paginationQuery.Size = _paginationConfiguration.DefaultSize;
        }
    }
}