using System.Reflection;
using Domain.Clients;
using Domain.Devices;
using Domain.RepairOrders;
using Domain.RepairParts;
using Domain.Technicians;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Client> Clients { get; init; }
    public DbSet<Technician> Technicians { get; init; }
    public DbSet<Device> Devices { get; init; }
    public DbSet<RepairOrder> RepairOrders { get; init; }
    public DbSet<RepairPart> RepairParts { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}