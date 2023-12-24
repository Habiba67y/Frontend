using AirBnB.Application.Common.Extensions;
using AirBnB.Application.Common.Locations.Services;
using AirBnB.DoMain.Common.Query;
using AirBnB.DoMain.Entites;
using AirBnB.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace AirBnb.Infrastructure.Common.Locations.Services;

public class LocationService(ILocationRepository locationRepository, IUrlService urlService) : ILocationService
{
    public IQueryable<Location> Get(
        Expression<Func<Location, bool>>? predicate = null,
        bool asNoTracking = false) =>
    locationRepository.Get(predicate, asNoTracking);

    public ValueTask<IList<Location>> GetAsync(
        QuerySpecification<Location> querySpecification,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
    locationRepository.GetAsync(querySpecification, asNoTracking, cancellationToken);

    public ValueTask<Location> GetByIdAsync(
        Guid id,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
    locationRepository.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<IList<Location>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
    locationRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public ValueTask<Location> CreateAsync(
        Location location,
        bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
    locationRepository.CreateAsync(location, saveChanges, cancellationToken);

    public ValueTask<Location> UpdateAsync(
        Location location,
        bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
    locationRepository.UpdateAsync(location, saveChanges, cancellationToken);

    public ValueTask<Location> DeleteAsync(
        Location location,
        bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
    locationRepository.DeleteAsync(location, saveChanges, cancellationToken);

    public ValueTask<Location> DeleteByIdAsync(
        Guid id,
        bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
    locationRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);

    public async ValueTask<Location> UploadImageAsync(
        Guid id,
        IFormFile formFile,
        string absolutePath,
        CancellationToken cancellationToken = default)
    {
        var location = await GetByIdAsync(id, false, cancellationToken) ?? throw new InvalidOperationException();

        var relativePath = Path.Combine(id.ToString(), formFile.Name);

        if (formFile.Length > 0)
        {
            using (var fileStream = new FileStream(Path.Combine(absolutePath, relativePath), FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
        }
        location.ImageUrl = await urlService.CreateUrlAsync(relativePath);
        return await UpdateAsync(location, true, cancellationToken);
    }
} 


