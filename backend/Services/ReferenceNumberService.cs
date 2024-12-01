using Backend.Database;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public interface IReferenceNumberService
{
    Task<string> GenerateReferenceNumberAsync(CancellationToken ct = default);
}

public class ReferenceNumberService(AppDbContext db) : IReferenceNumberService
{
    private const string ReferencePrefix = "KN";

    public async Task<string> GenerateReferenceNumberAsync(CancellationToken ct = default)
    {
        var lastTodayTransaction = await db
            .Deliveries.Where(t => t.CreatedAt.Date == DateTime.Now.Date)
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefaultAsync(ct);
        var lastNumber = 0;
        if (
            lastTodayTransaction is not null and { ReferenceNumber: not null }
            && lastTodayTransaction.ReferenceNumber != string.Empty
        )
        {
            var lastNumberStr = lastTodayTransaction.ReferenceNumber?.Split('-')?[2] ?? "0";
            lastNumber = int.Parse(lastNumberStr);
        }
        lastNumber++;
        var datePart = DateTime.Now.ToString("yyyyMMdd");
        return $"{ReferencePrefix}-{datePart}-{lastNumber:D6}";
    }
}
