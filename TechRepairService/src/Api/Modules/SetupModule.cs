namespace Api.Modules;

public static class SetupModule
{
    public static void SetupServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors();
    }

    public static void ConfigureCors(this IApplicationBuilder app)
    {
        app.UseCors(policy => policy
            .SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
    }
}