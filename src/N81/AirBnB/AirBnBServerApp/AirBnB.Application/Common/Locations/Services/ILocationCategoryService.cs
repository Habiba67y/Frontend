using AirBnB.DoMain.Common.Query;
using AirBnB.DoMain.Entites;
using System.Linq.Expressions;

namespace AirBnB.Application.Common.Locations.Services;

public interface ILocationCategoryService
{
    IQueryable<LocationCategory> Get(Expression<Func<LocationCategory, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<IList<LocationCategory>> GetAsync(
        QuerySpecification<LocationCategory> querySpecification,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<LocationCategory> GetByIdAsync(
        Guid id,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<IList<LocationCategory>> GetByIdsAsync(
            IEnumerable<Guid> ids,
            bool asNoTracking = false,
            CancellationToken cancellationToken = default
    );

    ValueTask<LocationCategory> CreateAsync(
            LocationCategory locationCategory,
            bool saveChanges = true,
            CancellationToken cancellationToken = default
    );

    ValueTask<LocationCategory> UpdateAsync(
            LocationCategory locationCategory,
            bool saveChanges = true,
            CancellationToken cancellationToken = default
    );

    ValueTask<LocationCategory> DeleteAsync(
            LocationCategory locationCategory,
            bool saveChanges = true,
            CancellationToken cancellationToken = default
    );

    ValueTask<LocationCategory> DeleteByIdAsync(
            Guid id,
            bool saveChanges = true,
            CancellationToken cancellationToken = default
    );
}
