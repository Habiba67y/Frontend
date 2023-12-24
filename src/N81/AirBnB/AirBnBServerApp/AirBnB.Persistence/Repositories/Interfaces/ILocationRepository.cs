using AirBnB.DoMain.Common.Entities;
using AirBnB.DoMain.Common.Query;
using AirBnB.DoMain.Entites;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories.Interfaces;

public interface ILocationRepository
{
    IQueryable<Location> Get(Expression<Func<Location, bool>>? predicate = default, bool asNoTracking = false);
    
    ValueTask<IList<Location>> GetAsync(
        QuerySpecification<Location> querySpecification,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<Location> GetByIdAsync(
        Guid id,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<IList<Location>> GetByIdsAsync(
            IEnumerable<Guid> ids,
            bool asNoTracking = false,
            CancellationToken cancellationToken = default
    );

    ValueTask<Location> CreateAsync(
            Location location,
            bool saveChanges = true,
            CancellationToken cancellationToken = default
    );

    ValueTask<Location> UpdateAsync(
            Location location,
            bool saveChanges = true,
            CancellationToken cancellationToken = default
    );

    ValueTask<Location> DeleteAsync(
            Location location,
            bool saveChanges = true,
            CancellationToken cancellationToken = default
    );

    ValueTask<Location> DeleteByIdAsync(
            Guid id,
            bool saveChanges = true,
            CancellationToken cancellationToken = default
    );
}
