using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.RepairOrders;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class RepairOrderRepository(ApplicationDbContext context) : IRepairOrderRepository, IRepairOrderQueries
{
    public async Task<IReadOnlyList<RepairOrder>> GetAll(CancellationToken cancellationToken)
    {
        return await context.RepairOrders
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<RepairOrder>> GetById(RepairOrderId id, CancellationToken cancellationToken)
    {
        var entity = await context.RepairOrders
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity ?? Option<RepairOrder>.None;
    }

    public async Task<Option<RepairOrder>> GetByOrderNumber(string orderNumber, CancellationToken cancellationToken)
    {
        var entity = await context.RepairOrders
            .FirstOrDefaultAsync(x => x.OrderNumber == orderNumber, cancellationToken);

        return entity ?? Option<RepairOrder>.None;
    }

    public async Task<RepairOrder> Add(RepairOrder repairOrder, CancellationToken cancellationToken)
    {
        await context.RepairOrders.AddAsync(repairOrder, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return repairOrder;
    }

    public async Task<RepairOrder> Update(RepairOrder repairOrder, CancellationToken cancellationToken)
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