using AirBnB.DoMain.Common.Caching;
using AirBnB.DoMain.Common.Query;
using AirBnB.DoMain.Entites;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories;

public class LocationRepository(LocationDbContext dbContext, ICacheBroker cacheBroker) : EntityRepositoryBase<Location, LocationDbContext>(
    dbContext,
    cacheBroker,
    new CacheEntryOptions(null, null)
    ), ILocationRepository
{
    public IQueryable<Location> Get(
        Expression<Func<Location, bool>>? predicate = null, 
        bool asNoTracking = false) =>
    base.Get(predicate, asNoTracking);

    public ValueTask<IList<Location>> GetAsync(
        QuerySpecification<Location> querySpecification,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
    base.GetAsync(querySpecification, asNoTracking);

    public ValueTask<Location> GetByIdAsync(
        Guid id, bool asNoTracking = false, 
        CancellationToken cancellationToken = default) =>
    base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<IList<Location>> GetByIdsAsync(
        IEnumerable<Guid> ids, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default) =>
    base.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public ValueTask<Location> CreateAsync(
        Location location, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    base.CreateAsync(location, saveChanges, cancellationToken);

    public ValueTask<Location> UpdateAsync(
        Location location, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    base.UpdateAsync(location, saveChanges, cancellationToken);
   
    public ValueTask<Location> DeleteAsync(
        Location location, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    base.DeleteAsync(location, saveChanges, cancellationToken);

    public ValueTask<Location> DeleteByIdAsync(
        Guid id, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    base.DeleteByIdAsync(id, saveChanges, cancellationToken);
} 

