using Domain.RepairOrders;
using LanguageExt;

namespace Application.Common.Interfaces.Repositories;

public interface IRepairOrderRepository
{
    Task<Option<RepairOrder>> GetById(RepairOrderId id, CancellationToken cancellationToken);
    Task<Option<RepairOrder>> GetByOrderNumber(string orderNumber, CancellationToken cancellationToken);
    Task<RepairOrder> Add(RepairOrder repairOrder, CancellationToken cancellationToken);
    Task<RepairOrder> Update(RepairOrder repairOrder, CancellationToken cancellationToken);
    Task Delete(RepairOrder repairOrder, CancellationToken cancellationToken);
}