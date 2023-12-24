using AirBnB.DoMain.Common.Entities;
using AirBnB.DoMain.Common.Query;

namespace AirBnB.DoMain.Extensions;

public static class LinqExtension
{
    public static IQueryable<TSource> ApplySpecification<TSource>(this IQueryable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {
        source = source.ApplyPredicates(querySpecification).ApplyOrdering(querySpecification).ApplyPagination(querySpecification);

        return source;
    }

    public static IEnumerable<TSource> ApplySpecification<TSource>(this IEnumerable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {
        source = source.ApplyPredicates(querySpecification).ApplyOrdering(querySpecification).ApplyPagination(querySpecification);

        return source;
    }

    public static IQueryable<TSource> ApplyPredicates<TSource>(this IQueryable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {
        querySpecification.FilteringOptions.ForEach(predicate => source = source.Where(predicate));

        return source;
    }

    public static IEnumerable<TSource> ApplyPredicates<TSource>(this IEnumerable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {
        querySpecification.FilteringOptions.ForEach(predicate => source = source.Where(predicate.Compile()));

        return source;
    }

    public static IQueryable<TSource> ApplyOrdering<TSource>(this IQueryable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {
        if (!querySpecification.OrderingOptions.Any())
            source.OrderBy(entity => entity.Id);

        querySpecification.OrderingOptions.ForEach(
            orderByExpression => source = orderByExpression.isAscending
            ? source.OrderBy(orderByExpression.keySelector)
            : source.OrderByDescending(orderByExpression.keySelector)
            );

        return source;
    }

    public static IEnumerable<TSource> ApplyOrdering<TSource>(this IEnumerable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {
        if (!querySpecification.OrderingOptions.Any())
            source.OrderBy(entity => entity.Id);

        querySpecification.OrderingOptions.ForEach(
            orderByExpression => source = orderByExpression.isAscending
            ? source.OrderBy(orderByExpression.keySelector.Compile())
            : source.OrderByDescending(orderByExpression.keySelector.Compile())
            );

        return source;
    }

    public static IQueryable<TSource> ApplyPagination<TSource>(this IQueryable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {
        var token = (int)querySpecification.PaginationOptions.PageToken;
        var size = (int)querySpecification.PaginationOptions.PageSize;

        return source.Skip((token - 1) * size).Take(size);
    }

    public static IEnumerable<TSource> ApplyPagination<TSource>(this IEnumerable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {
        var token = (int)querySpecification.PaginationOptions.PageToken;
        var size = (int)querySpecification.PaginationOptions.PageSize;

        return source.Skip((token - 1) * size).Take(size);
    }
}
