namespace Redarbor.TechnicalTest.Domain.Abstractions;

public interface IEntity<T> : IEntity
{
    public T Id { get; set; }
}

public interface IEntity
{
    public DateTime? CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
}
