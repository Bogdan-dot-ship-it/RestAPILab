using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public sealed class ApplicationDbContextInitialiser(
    ILogger<ApplicationDbContextInitialiser> logger,
    ApplicationDbContext dbContext)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error occurred while migrating the database.");
            throw;
        }
    }
}
