using Domain.RepairOrders;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface IRepairOrderQueries
{
    Task<IReadOnlyList<RepairOrder>> GetAll(CancellationToken cancellationToken);
    Task<Option<RepairOrder>> GetById(RepairOrderId id, CancellationToken cancellationToken);
}