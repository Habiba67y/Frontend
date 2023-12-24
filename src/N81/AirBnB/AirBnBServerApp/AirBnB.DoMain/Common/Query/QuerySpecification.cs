using AirBnB.DoMain.Common.Caching;
using AirBnB.DoMain.Common.Entities;
using AirBnB.DoMain.Comparers;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace AirBnB.DoMain.Common.Query;

public class QuerySpecification<TEntity>(uint pageSize, uint pageToken) : CacheModel where TEntity : IEntity
{
    public List<Expression<Func<TEntity, bool>>> FilteringOptions { get; } = new();
    public List<(Expression<Func<TEntity, object>> keySelector, bool isAscending)> OrderingOptions { get; } = new();

    public FilterPagination PaginationOptions { get; set; } = new(pageToken, pageSize);

    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        foreach(var filter in FilteringOptions.Order(new PredicateExpressionComparer<TEntity>()))
            hashCode.Add(filter.GetHashCode());

        foreach(var filter in OrderingOptions)
            hashCode.Add(filter.GetHashCode());

        hashCode.Add(PaginationOptions);

        return hashCode.ToHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is QuerySpecification<TEntity> querySpecification && querySpecification.GetHashCode() == GetHashCode();
    }

    public override string CacheKey => $"{typeof(TEntity).Name}_{GetHashCode()}";
}
