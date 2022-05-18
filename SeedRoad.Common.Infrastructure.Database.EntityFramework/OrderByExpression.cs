using System.Linq.Expressions;
using SeedRoad.Common.Core.Application.Pagination;

namespace SeedRoad.Common.Infrastructure.Database.EntityFramework;

public record OrderByExpression<T, TKey>(Expression<Func<T, TKey>> KeySelector, OrderEnum? Order);

public record OrderByExpression<T>
    (Expression<Func<T, object?>> KeySelector, OrderEnum? Order) : OrderByExpression<T, object?>(KeySelector, Order);