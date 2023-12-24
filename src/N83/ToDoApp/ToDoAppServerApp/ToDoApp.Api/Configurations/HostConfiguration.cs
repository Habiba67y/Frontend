namespace ToDoApp.Api.Configurations;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddValidators()
            .AddMappers()
            .AddBusinessLogicInfrastructure()
            .AddExposers()
            .AddCors()
            .AddDevTools();

        return new(builder);
    }

    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        await app.MigrateDatabaseAsync();

        app
            .UseCors();
        app
            .UseExposers()
            .UseDevTools();
        
        return app;
    }
}
