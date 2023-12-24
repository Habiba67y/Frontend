using AirBnB.DoMain.Common.Caching;
using AirBnB.DoMain.Common.Query;
using AirBnB.DoMain.Entites;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories;

public class LocationCategoryRepository(LocationDbContext dbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<LocationCategory, LocationDbContext>(
        dbContext,
        cacheBroker,
        new CacheEntryOptions(null, null)), ILocationCategoryRepository
{
    public IQueryable<LocationCategory> Get(
        Expression<Func<LocationCategory, bool>>? predicate = null,
        bool asNoTracking = false) =>
    base.Get(predicate, asNoTracking);

    public ValueTask<IList<LocationCategory>> GetAsync(
        QuerySpecification<LocationCategory> querySpecification,
        bool asNoTracking = false, CancellationToken cancellationToken = default) =>
    base.GetAsync(querySpecification, asNoTracking, cancellationToken);

    public ValueTask<LocationCategory> GetByIdAsync(
        Guid id,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
    base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<IList<LocationCategory>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
    base.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public ValueTask<LocationCategory> CreateAsync(
        LocationCategory locationCategory,
        bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
    base.CreateAsync(locationCategory, saveChanges, cancellationToken);

    public ValueTask<LocationCategory> UpdateAsync(
        LocationCategory locationCategory, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    base.UpdateAsync(locationCategory, saveChanges, cancellationToken);

    public ValueTask<LocationCategory> DeleteAsync(
        LocationCategory locationCategory, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    base.DeleteAsync(locationCategory, saveChanges, cancellationToken);

    public ValueTask<LocationCategory> DeleteByIdAsync(
        Guid id, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    base.DeleteByIdAsync(id, saveChanges, cancellationToken);
}

