namespace Domain.Clients;

public class Client
{
    public Guid Id { get; }
    public string FullName { get; private set; }
    public string Phone { get; private set; }
    public string? Email { get; private set; }
    public DateTime CreatedAt { get; }

    private Client(Guid id, string fullName, string phone, string? email, DateTime createdAt)
        => (Id, FullName, Phone, Email, CreatedAt) = (id, fullName, phone, email, createdAt);

    public static Client New(Guid id, string fullName, string phone, string? email = null)
        => new(id, fullName, phone, email, DateTime.UtcNow);

    public void UpdateDetails(string fullName, string phone, string? email)
        => (FullName, Phone, Email) = (fullName, phone, email);
}