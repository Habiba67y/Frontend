using AirBnb.Infrastructure.Common.Settings;
using AirBnB.Application.Common.Extensions;
using AirBnB.Application.Common.Locations.Services;
using Microsoft.Extensions.Options;
using static System.Net.WebRequestMethods;

namespace AirBnb.Infrastructure.Common.Locations.Services;

public class UrlService(IOptions<ApplicationUrlSettings> applicationUrlSettings) : IUrlService
{
    public async ValueTask<string> CreateUrlAsync(string relativePath)
    {
        return new($"{applicationUrlSettings.Value.Url}/{await relativePath.ToUrlAsync()}");
    }
}
