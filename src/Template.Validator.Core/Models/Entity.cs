namespace Template.Validator.Core.Models;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        ModifiedAt = DateTime.Now;
    }
}
