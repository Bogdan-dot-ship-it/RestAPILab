using System.ComponentModel.DataAnnotations;
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
    DateTime CreatedAt)
{
    public static RepairOrderDto FromDomainModel(RepairOrder order) =>
        new(
            order.Id,
            order.OrderNumber,
            order.DeviceModel,
            order.EstimatedCost,
            order.IsUrgent,
            order.Status,
            order.EstimatedCompletionDate,
            order.CreatedAt);
}

public record CreateRepairOrderDto(
    [Required, MaxLength(50)] string OrderNumber,
    [Required, MaxLength(255)] string DeviceModel,
    [Range(0.01, 1_000_000)] decimal EstimatedCost,
    [Required] bool IsUrgent,
    [Required, EnumDataType(typeof(RepairStatus))] RepairStatus Status,
    [Required] DateTime EstimatedCompletionDate);

public record UpdateRepairOrderDto(
    [Required, MaxLength(50)] string OrderNumber,
    [Required, MaxLength(255)] string DeviceModel,
    [Range(0.01, 1_000_000)] decimal EstimatedCost,
    [Required] bool IsUrgent,
    [Required, EnumDataType(typeof(RepairStatus))] RepairStatus Status,
    [Required] DateTime EstimatedCompletionDate);