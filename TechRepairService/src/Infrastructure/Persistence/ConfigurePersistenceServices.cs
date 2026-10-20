using Application.Common.Interfaces;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure.Persistence;

public static class ConfigurePersistenceServices
{
    public static void AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' was not found.");

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.EnableDynamicJson();
        var dataSource = dataSourceBuilder.Build();

        services.AddSingleton(dataSource);
        services.AddDbContext<ApplicationDbContext>(options => options
            .UseNpgsql(dataSource, npgsql => npgsql.MigrationsAssembly(
                typeof(ApplicationDbContext).Assembly.FullName))
            .UseSnakeCaseNamingConvention());

        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddScoped<IRepairOrderRepository, RepairOrderRepository>();
    }
}
