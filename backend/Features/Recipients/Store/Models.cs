namespace Backend.Features.Recipients.Store;

public class RecipientStoreReq : RecipientModel { }

public class RecipientStoreRes : RecipientModel
{
    public Guid Id { get; set; }
}
