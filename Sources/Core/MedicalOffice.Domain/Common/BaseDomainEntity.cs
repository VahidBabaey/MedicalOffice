using MedicalOffice.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

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

public interface IPrimaryKeyEntity<T> where T : struct
{
    public T Id { get; set; }
}

public interface IAuditableEntity
{
    public DateTime CreatedDate { get; set; }
    public Guid CreatedById { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public Guid LastUpdatedById { get; set; }
}

public interface ISoftDeletableEntity
{
    public bool IsDeleted { get; set; }
}