namespace Backend.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    public DateTime AccessExpiry { get; set; }
    public DateTime RefreshExpiry { get; set; }
}
