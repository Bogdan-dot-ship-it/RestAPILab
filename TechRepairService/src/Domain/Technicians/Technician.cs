namespace Domain.Technicians;

public class Technician
{
    public Guid Id { get; }
    public string FullName { get; private set; }
    public string Specialization { get; private set; }
    public string Phone { get; private set; }
    public bool IsActive { get; private set; }

    private Technician(Guid id, string fullName, string specialization, string phone, bool isActive)
        => (Id, FullName, Specialization, Phone, IsActive) = (id, fullName, specialization, phone, isActive);

    public static Technician New(Guid id, string fullName, string specialization, string phone, bool isActive = true)
        => new(id, fullName, specialization, phone, isActive);

    public void UpdateDetails(string fullName, string specialization, string phone, bool isActive)
        => (FullName, Specialization, Phone, IsActive) = (fullName, specialization, phone, isActive);
}