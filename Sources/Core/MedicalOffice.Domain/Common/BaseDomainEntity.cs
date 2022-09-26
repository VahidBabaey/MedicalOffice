namespace MedicalOffice.Domain.Common;

public class BaseDomainEntity<T> where T : struct
{
    public T Id { get; set; } = default;
    public DateTime CreatedDate { get; set; } = default;
    public Guid CreatedById { get; set; } = default;
    public DateTime LastUpdatedDate { get; set; } = default;
    public Guid LastUpdatedById { get; set; } = default;
    public bool IsDeleted { get; set; } = default;
}