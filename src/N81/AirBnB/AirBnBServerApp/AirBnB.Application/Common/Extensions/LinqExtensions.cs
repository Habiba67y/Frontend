using AirBnB.DoMain.Common.Query;

namespace AirBnB.Application.Common.Extensions;

public static class LinqExtensions
{
    public static IQueryable<TSource> ApplyPagination<TSource>(this IQueryable<TSource> source, FilterPagination filterPagination) =>
        source.Skip((int)(filterPagination.PageToken - 1) * (int)filterPagination.PageSize).Take((int)filterPagination.PageSize);

    public static IEnumerable<TSource> ApplyPagination<TSource>(this IEnumerable<TSource> source, FilterPagination filterPagination) =>
        source.Skip((int)(filterPagination.PageToken - 1) * (int)filterPagination.PageSize).Take((int)filterPagination.PageSize);
}
