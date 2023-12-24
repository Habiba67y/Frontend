using AirBnb.Infrastructure.Common.Caching.Brokers;
using AirBnb.Infrastructure.Common.Locations.Services;
using AirBnb.Infrastructure.Common.Settings;
using AirBnB.Application.Common.Locations.Services;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories;
using AirBnB.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AirBnB.Api.Configurations;

public static partial class HostConfiguration
{

    private static readonly ICollection<Assembly> Assemblies;

    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    private static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));

        builder.Services.AddStackExchangeRedisCache(
            options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
                options.InstanceName = "AirBnB";
            }
        );

        builder.Services
            .AddSingleton<ICacheBroker, RedisDistributedCacheBroker>();

        return builder;
    }

    private static WebApplicationBuilder AddLocationsInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LocationDbContext>(
            options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder.Services.Configure<ApplicationUrlSettings>(builder.Configuration.GetSection(nameof(ApplicationUrlSettings)));

        builder.Services
            .AddScoped<ILocationRepository, LocationRepository>()
            .AddScoped<ILocationCategoryRepository, LocationCategoryRepository>();

        builder.Services
            .AddScoped<IUrlService, UrlService>()
            .AddScoped<ILocationService, LocationService>()
            .AddScoped<ILocationCategoryService, LocationCategoryService>();

        return builder;
    }

    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        // register automapper
        builder.Services.AddAutoMapper(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}
