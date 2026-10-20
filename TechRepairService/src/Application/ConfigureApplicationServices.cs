using Application.RepairOrders.Services.Abstract;
using Application.RepairOrders.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureApplicationServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRepairOrderService, RepairOrderService>();
    }
}