namespace Domain.Entities;

public class BaseEntity {

    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = Teste();

    public static DateTime Teste() {
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

        // Obtenha a data e hora atual em Brasília
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
    }
}
