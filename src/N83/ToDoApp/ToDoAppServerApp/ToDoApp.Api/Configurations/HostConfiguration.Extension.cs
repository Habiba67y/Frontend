using FluentValidation;
using LocalIdentity.SimpleInfra.Api.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoApp.Application.ToDos.Services;
using ToDoApp.Infrastructure.ToDos.Services;
using ToDoApp.Persistence.DataContexts;
using ToDoApp.Persistence.Repositories;
using ToDoApp.Persistence.Repositories.Interfaces;

namespace ToDoApp.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies;

    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblies(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddBusinessLogicInfrastructure(this WebApplicationBuilder builder)
    {
        var connection = builder.Configuration.GetConnectionString("AppDatabaseConnection");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("AppDatabaseConnection")));

        builder.Services
            .AddScoped<IToDoRepository, ToDoRepository>();

        builder.Services
            .AddScoped<IToDoService, ToDoService>();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers().AddNewtonsoftJson();

        return builder;
    }

    private static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => { 
                options.AddDefaultPolicy(
                policyBuilder => 
                { 
                    policyBuilder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader(); 
                }
                ); 
            }
        );

        return builder;
    }
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    private static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
    {
        var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        await scopeFactory.MigrateAsync<AppDbContext>();

        return app;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app) 
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}
