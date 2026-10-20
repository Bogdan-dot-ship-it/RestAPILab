using Domain.RepairOrders;

namespace Api.Dtos;

public record RepairOrderDto(
    Guid Id,
    string OrderNumber,
    string DeviceModel,
    decimal EstimatedCost,
    bool IsUrgent,
    RepairStatus Status,
    DateTime EstimatedCompletionDate,
    DateTime CreatedAt,
    Guid? DeviceId = null,
    Guid? TechnicianId = null)
{
    public static RepairOrderDto FromDomainModel(RepairOrder order)
        => new(
            order.Id.Value,
            order.OrderNumber,
            order.DeviceModel,
            order.EstimatedCost,
            order.IsUrgent,
            order.Status,
            order.EstimatedCompletionDate,
            order.CreatedAt,
            order.DeviceId,
            order.TechnicianId);
}

public record CreateRepairOrderDto(
    string OrderNumber,
    string DeviceModel,
    decimal EstimatedCost,
    bool IsUrgent,
    RepairStatus Status,
    DateTime EstimatedCompletionDate,
    Guid? DeviceId = null,
    Guid? TechnicianId = null);

public record UpdateRepairOrderDto(
    string OrderNumber,
    string DeviceModel,
    decimal EstimatedCost,
    bool IsUrgent,
    RepairStatus Status,
    DateTime EstimatedCompletionDate,
    Guid? DeviceId = null,
    Guid? TechnicianId = null);