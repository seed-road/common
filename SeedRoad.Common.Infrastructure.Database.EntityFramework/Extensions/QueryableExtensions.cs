using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SeedRoad.Common.Core.Application.Pagination;

namespace SeedRoad.Common.Infrastructure.Database.EntityFramework.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedList<T>> FromPaginationQueryAsync<T>(this IQueryable<T> query,
        IPagination pagination)
    {
        var count = await query.CountAsync();
        var skip = pagination.Page * pagination.Size;
        var items = await query.Skip(skip).Take(pagination.Size).ToListAsync();
        return new PagedList<T>(items, count, pagination.Page, pagination.Size);
    }

    public static IOrderedQueryable<T> OrderByEnum<T, TKey>(this IQueryable<T> query,
        Expression<Func<T, TKey>> keySelector, OrderEnum orderEnum)
    {
        return orderEnum switch
        {
            OrderEnum.Asc => query.OrderBy(keySelector),
            OrderEnum.Desc => query.OrderByDescending(keySelector),
            _ => throw new ArgumentOutOfRangeException(nameof(orderEnum), orderEnum, null)
        };
    }

    public static IQueryable<T> OrderByEnum<T, TKey>(this IQueryable<T> query,
        Expression<Func<T, TKey>> keySelector, OrderEnum? orderEnum)
    {
        return orderEnum switch
        {
            OrderEnum.Asc => query.OrderBy(keySelector),
            OrderEnum.Desc => query.OrderByDescending(keySelector),
            _ => query
        };
    }

    public static IQueryable<T> OrderByExpressions<T>(this IQueryable<T> query,
        params OrderByExpression<T, object?>[] orderByExpressions)
    {
        if (!orderByExpressions.Any())
        {
            return query;   
        }

        var i = Array.FindIndex(orderByExpressions, o => o.Order != null);
        if (i < 0)
        {
            return query;
        }

        var orderedQuery = query.OrderByEnum(orderByExpressions[i].KeySelector, orderByExpressions[i].Order!.Value);
        for (var j = i + 1; j < orderByExpressions.Length; j++)
        {
            if (orderByExpressions[j].Order == null)
            {
                continue;
            }

            orderedQuery = orderByExpressions[j].Order switch
            {
                OrderEnum.Asc => orderedQuery.ThenBy(orderByExpressions[j].KeySelector),
                OrderEnum.Desc => orderedQuery.ThenByDescending(orderByExpressions[j].KeySelector),
                _ => throw new ArgumentOutOfRangeException(nameof(OrderByExpression<T, object>),
                    "Invalid order enumeration")
            };
        }

        return orderedQuery;
    }
}