using AirBnB.Api.Locations.Models;
using AirBnB.Api.Models.Dtos;
using AirBnB.Application.Common.Locations.Services;
using AirBnB.DoMain.Common.Query;
using AirBnB.DoMain.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirBnB.Api.Controllers;
[ApiController]
[Route("api/[controller]")]

public class LocationsController(
    IMapper mapper,
    ILocationService locationService,
    ILocationCategoryService locationCategoryService
) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetLocations([FromQuery] FilterPagination filterPagination, CancellationToken cancellationToken = default)
    {
        var a = mapper;
        var specification = new QuerySpecification<Location>(filterPagination.PageSize, filterPagination.PageToken);

        var result = await locationService.GetAsync(specification, true, cancellationToken);

        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("locationCategories")]
    public async ValueTask<IActionResult> GetLocationCategories([FromQuery] FilterPagination filterPagination, CancellationToken cancellationToken = default)
    {
        var specification = new QuerySpecification<LocationCategory>(filterPagination.PageSize, filterPagination.PageToken);

        var result = await locationCategoryService.GetAsync(specification, true, cancellationToken);

        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("{locationId:guid}")]
    public async ValueTask<IActionResult> GetLocationById([FromRoute] Guid locationId, CancellationToken cancellationToken = default)
    {
        var result = await locationService.GetByIdAsync(locationId, true, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet("locationCategories/{locationCategoryId:guid}")]
    public async ValueTask<IActionResult> GetLocationCategoryById([FromRoute] Guid locationCategoryId, CancellationToken cancellationToken = default)
    {
        var result = await locationCategoryService.GetByIdAsync(locationCategoryId, true, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateLocation([FromBody] LocationDto locationDto, CancellationToken cancellationToken)
    {
        var location = mapper.Map<Location>(locationDto);
        var result = await locationService.CreateAsync(location, cancellationToken: cancellationToken);

        return CreatedAtAction(nameof(GetLocationById), new { LocationId = result.Id }, result);
    }

    [HttpPost("locationCategories")]
    public async ValueTask<IActionResult> CreateLocation([FromBody] LocationCategoryDto locationCategoryDto, CancellationToken cancellationToken)
    {
        var locationCategory = mapper.Map<LocationCategory>(locationCategoryDto);
        var result = await locationCategoryService.CreateAsync(locationCategory, cancellationToken: cancellationToken);

        return CreatedAtAction(nameof(GetLocationCategoryById), new { LocationCategoryId = result.Id }, result);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateLocation([FromBody] LocationDto locationDto, CancellationToken cancellationToken)
    {
        var location = mapper.Map<Location>(locationDto);
        await locationService.UpdateAsync(location, cancellationToken: cancellationToken);

        return NoContent();
    }

    [HttpPut("locationCategories")]
    public async ValueTask<IActionResult> UpdateLocationCategory([FromBody] LocationCategoryDto locationCategoryDto, CancellationToken cancellationToken)
    {
        var locationCategory = mapper.Map<LocationCategory>(locationCategoryDto);
        await locationCategoryService.UpdateAsync(locationCategory, cancellationToken: cancellationToken);

        return NoContent();
    }

    [HttpDelete]
    public async ValueTask<IActionResult> DeleteLocation([FromBody] LocationDto locationDto, CancellationToken cancellationToken)
    {
        var location = mapper.Map<Location>(locationDto);
        await locationService.DeleteAsync(location, cancellationToken: cancellationToken);

        return NoContent();
    }

    [HttpDelete("locationCategories")]
    public async ValueTask<IActionResult> DeleteLocationCategory([FromBody] LocationCategoryDto locationCategoryDto, CancellationToken cancellationToken)
    {
        var locationCategory = mapper.Map<LocationCategory>(locationCategoryDto);
        await locationCategoryService.DeleteAsync(locationCategory, cancellationToken: cancellationToken);

        return NoContent();
    }

    //[httpput("location/{locationid:guid}/upload")]
    //public async valuetask<iactionresult> uploadimage([fromroute] guid locationid, [frombody] iformfile formfile, [fromservices] iwebhostenvironment environment, cancellationtoken cancellationtoken)
    //{
    //    var result = await locationservice.uploadimageasync(locationid, formfile, environment.webrootpath, cancellationtoken);

    //    return nocontent();
    //}
}

