namespace Domain.RepairOrders;

public record RepairOrderId(Guid Value)
{
    public static RepairOrderId Empty() => new(Guid.Empty);
    public static RepairOrderId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}