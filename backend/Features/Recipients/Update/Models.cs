namespace Backend.Features.Recipients.Update;

public class RecipientUpdateReq : RecipientModel { }

public class RecipientUpdateRes : RecipientModel
{
    public Guid Id { get; set; }
}
