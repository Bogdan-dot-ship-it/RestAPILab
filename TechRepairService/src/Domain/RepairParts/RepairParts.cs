namespace Domain.RepairParts;

public class RepairPart
{
    public Guid Id { get; }
    public Guid RepairOrderId { get; private set; }
    public string PartName { get; private set; }
    public decimal Cost { get; private set; }
    public int Quantity { get; private set; }

    private RepairPart(Guid id, Guid repairOrderId, string partName, decimal cost, int quantity)
        => (Id, RepairOrderId, PartName, Cost, Quantity) = (id, repairOrderId, partName, cost, quantity);

    public static RepairPart New(Guid id, Guid repairOrderId, string partName, decimal cost, int quantity = 1)
        => new(id, repairOrderId, partName, cost, quantity);

    public void UpdateDetails(string partName, decimal cost, int quantity)
        => (PartName, Cost, Quantity) = (partName, cost, quantity);
}