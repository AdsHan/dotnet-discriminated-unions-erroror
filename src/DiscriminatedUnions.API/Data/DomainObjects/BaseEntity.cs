namespace DiscriminatedUnions.API.Data.DomainObjects;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime DateCreateAt { get; private set; }

    protected BaseEntity()
    {
        DateCreateAt = DateTime.Now;
    }
}