using Application.Common.Interfaces;
using Domain.RepairOrders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class RepairOrderRepository(ApplicationDbContext context) : IRepairOrderRepository
{
    public async Task<IReadOnlyList<RepairOrder>> GetAll(CancellationToken cancellationToken)
    {
        return await context.RepairOrders.ToListAsync(cancellationToken);
    }

    public async Task<RepairOrder?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context.RepairOrders.FirstOrDefaultAsync(
            x => x.Id == id,
            cancellationToken);
    }

    public async Task<RepairOrder?> GetByOrderNumber(
        string orderNumber,
        CancellationToken cancellationToken)
    {
        return await context.RepairOrders.FirstOrDefaultAsync(
            x => x.OrderNumber == orderNumber,
            cancellationToken);
    }

    public async Task<RepairOrder> Add(
        RepairOrder repairOrder,
        CancellationToken cancellationToken)
    {
        await context.RepairOrders.AddAsync(repairOrder, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return repairOrder;
    }

    public async Task<RepairOrder> Update(
        RepairOrder repairOrder,
        CancellationToken cancellationToken)
    {
        context.RepairOrders.Update(repairOrder);
        await context.SaveChangesAsync(cancellationToken);
        return repairOrder;
    }

    public async Task Delete(RepairOrder repairOrder, CancellationToken cancellationToken)
    {
        context.RepairOrders.Remove(repairOrder);
        await context.SaveChangesAsync(cancellationToken);
    }
}
