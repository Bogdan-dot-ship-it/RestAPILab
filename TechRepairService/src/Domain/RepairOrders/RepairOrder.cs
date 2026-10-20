namespace Domain.RepairOrders;

public class RepairOrder
{
    public RepairOrderId Id { get; }
    public Guid? DeviceId { get; private set; }
    public Guid? TechnicianId { get; private set; }
    public string OrderNumber { get; private set; }
    public string DeviceModel { get; private set; }
    public decimal EstimatedCost { get; private set; }
    public bool IsUrgent { get; private set; }
    public RepairStatus Status { get; private set; }
    public DateTime EstimatedCompletionDate { get; private set; }
    public DateTime CreatedAt { get; }

    private RepairOrder(
        RepairOrderId id,
        Guid? deviceId,
        Guid? technicianId,
        string orderNumber,
        string deviceModel,
        decimal estimatedCost,
        bool isUrgent,
        RepairStatus status,
        DateTime estimatedCompletionDate,
        DateTime createdAt)
        => (Id, DeviceId, TechnicianId, OrderNumber, DeviceModel, EstimatedCost, IsUrgent, Status, EstimatedCompletionDate, CreatedAt)
            = (id, deviceId, technicianId, orderNumber, deviceModel, estimatedCost, isUrgent, status, estimatedCompletionDate, createdAt);

    public static RepairOrder New(
        RepairOrderId id,
        string orderNumber,
        string deviceModel,
        decimal estimatedCost,
        bool isUrgent,
        RepairStatus status,
        DateTime estimatedCompletionDate,
        Guid? deviceId = null,
        Guid? technicianId = null)
        => new(id, deviceId, technicianId, orderNumber, deviceModel, estimatedCost, isUrgent, status, estimatedCompletionDate, DateTime.UtcNow);

    public void UpdateDetails(
        string deviceModel,
        decimal estimatedCost,
        bool isUrgent,
        RepairStatus status,
        DateTime estimatedCompletionDate,
        Guid? deviceId = null,
        Guid? technicianId = null)
    {
        DeviceModel = deviceModel;
        EstimatedCost = estimatedCost;
        IsUrgent = isUrgent;
        Status = status;
        EstimatedCompletionDate = estimatedCompletionDate;
        DeviceId = deviceId;
        TechnicianId = technicianId;
    }
}