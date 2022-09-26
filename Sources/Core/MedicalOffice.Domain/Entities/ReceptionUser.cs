using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// کاربران / دکتران پذیرش
/// </summary>
public class ReceptionUser : BaseDomainEntity<Guid>
{
    /// <summary>
    /// درصد و سهم پزشک
    /// </summary>
    public UserServiceSharePercent? UserServiceSharePercent { get; set; }
    /// <summary>
    /// آیدی درصد و سهم پزشک
    /// </summary>
    public Guid UserServiceSharePercentId { get; set; }
    /// <summary>
    /// کاربر لاگین شده
    /// </summary>
    public User? User { get; set; }
    /// <summary>
    /// آیدی کاربر لاگین شده
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// جزئیات پذیرش
    /// </summary>
    public ReceptionDetail? ReceptionDetail { get; set; }
    /// <summary>
    /// آیدی جزئیات پذیرش
    /// </summary>
    public Guid ReceptionDetailId { get; set; }
}