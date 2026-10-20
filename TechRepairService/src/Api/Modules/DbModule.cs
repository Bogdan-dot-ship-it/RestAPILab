using Infrastructure.Persistence;

namespace Api.Modules;

public static class DbModule
{
    public static async Task InitialiseDatabaseAsync(this WebApplication application)
    {
        await using var scope = application.Services.CreateAsyncScope();
        var initialiser = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
    }
}
