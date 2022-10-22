using MedicalOffice.Application.Dtos.Reception;
using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// کاربران / دکتران پذیرش
/// </summary>
public class ReceptionUserDTO : BaseDomainEntity<Guid>
{

    /// <summary>
    /// آیدی درصد و سهم پزشک
    /// </summary>
    public Guid UserServiceSharePercentId { get; set; }
    /// <summary>
    /// کاربر لاگین شده
    /// </summary>
    public MedicalStaff? User { get; set; }
    /// <summary>
    /// آیدی کاربر لاگین شده
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// جزئیات پذیرش
    /// </summary>
    public ReceptionDetailDTO? ReceptionDetail { get; set; }
    /// <summary>
    /// آیدی جزئیات پذیرش
    /// </summary>
    public Guid ReceptionDetailId { get; set; }
}