using AirBnB.Application.Common.Locations.Services;
using AirBnB.DoMain.Common.Query;
using AirBnB.DoMain.Entites;
using AirBnB.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace AirBnb.Infrastructure.Common.Locations.Services;

public class LocationCategoryService(ILocationCategoryRepository locationCategoryRepository) : ILocationCategoryService
{
    public IQueryable<LocationCategory> Get(
        Expression<Func<LocationCategory, bool>>? predicate = null, 
        bool asNoTracking = false) =>
    locationCategoryRepository.Get(predicate, asNoTracking);

    public ValueTask<IList<LocationCategory>> GetAsync(
        QuerySpecification<LocationCategory> querySpecification, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default) =>
    locationCategoryRepository.GetAsync(querySpecification, asNoTracking, cancellationToken);

    public ValueTask<LocationCategory> GetByIdAsync(
        Guid id, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default) =>
    locationCategoryRepository.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<IList<LocationCategory>> GetByIdsAsync(
        IEnumerable<Guid> ids, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default) =>
    locationCategoryRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public ValueTask<LocationCategory> CreateAsync(
        LocationCategory locationCategory, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    locationCategoryRepository.CreateAsync(locationCategory, saveChanges, cancellationToken);

    public ValueTask<LocationCategory> UpdateAsync(
        LocationCategory locationCategory, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    locationCategoryRepository.UpdateAsync(locationCategory, saveChanges, cancellationToken);

    public ValueTask<LocationCategory> DeleteAsync(
        LocationCategory locationCategory, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    locationCategoryRepository.DeleteAsync(locationCategory, saveChanges, cancellationToken);

    public ValueTask<LocationCategory> DeleteByIdAsync(
        Guid id, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    locationCategoryRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);
}
