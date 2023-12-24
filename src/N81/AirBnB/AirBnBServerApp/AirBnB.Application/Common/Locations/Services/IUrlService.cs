namespace AirBnB.Application.Common.Locations.Services;

public interface IUrlService
{
    ValueTask<string> CreateUrlAsync(string relativePath);
}
