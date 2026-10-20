namespace Domain.Devices;

public class Device
{
    public Guid Id { get; }
    public Guid ClientId { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public string SerialNumber { get; private set; }
    public string DeviceType { get; private set; }

    private Device(Guid id, Guid clientId, string brand, string model, string serialNumber, string deviceType)
        => (Id, ClientId, Brand, Model, SerialNumber, DeviceType) = (id, clientId, brand, model, serialNumber, deviceType);

    public static Device New(Guid id, Guid clientId, string brand, string model, string serialNumber, string deviceType)
        => new(id, clientId, brand, model, serialNumber, deviceType);

    public void UpdateDetails(string brand, string model, string serialNumber, string deviceType)
        => (Brand, Model, SerialNumber, DeviceType) = (brand, model, serialNumber, deviceType);
}