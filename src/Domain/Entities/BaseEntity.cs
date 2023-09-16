namespace Domain.Entities;

public class BaseEntity {

    public int Id { get; set; }
    public DateTime Created_At { get; set; } = DateTime.UtcNow;

}
