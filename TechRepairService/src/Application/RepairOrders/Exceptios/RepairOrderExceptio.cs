using Domain.RepairOrders;

namespace Application.RepairOrders.Exceptions;

public abstract class RepairOrderException(RepairOrderId repairOrderId, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public RepairOrderId RepairOrderId { get; } = repairOrderId;
}

public class RepairOrderAlreadyExistsException(RepairOrderId repairOrderId)
    : RepairOrderException(repairOrderId, $"Repair order already exists with order number under id {repairOrderId}");

public class RepairOrderNotFoundException(RepairOrderId repairOrderId)
    : RepairOrderException(repairOrderId, $"Repair order not found under id {repairOrderId}");

public class RepairOrderUnhandledException(RepairOrderId repairOrderId, Exception? innerException = null)
    : RepairOrderException(repairOrderId, $"Unhandled exception under id {repairOrderId}", innerException);