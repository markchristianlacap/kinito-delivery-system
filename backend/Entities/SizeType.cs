namespace Backend.Entities;

public class SizeType
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
