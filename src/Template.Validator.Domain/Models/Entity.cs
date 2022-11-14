namespace Template.Validator.Domain.Models;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime CriatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        CriatedAt = DateTime.Now;
        ModifiedAt = DateTime.Now;
    }
}
