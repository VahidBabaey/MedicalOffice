using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// کاربران / دکتران پذیرش
/// </summary>
public class ReceptionMedicalStaffDTO : BaseDomainEntity<Guid>
{

    /// <summary>
    /// آیدی درصد و سهم پزشک
    /// </summary>
    public Guid MedicalStaffServiceSharePercentId { get; set; }
    /// <summary>
    /// کاربر لاگین شده
    /// </summary>
    public MedicalStaff? MedicalStaff { get; set; }
    /// <summary>
    /// آیدی کاربر لاگین شده
    /// </summary>
    public Guid MedicalStaffId { get; set; }
    /// <summary>
    /// جزئیات پذیرش
    /// </summary>
    public ReceptionDetailDTO? ReceptionDetail { get; set; }
    /// <summary>
    /// آیدی جزئیات پذیرش
    /// </summary>
    public Guid ReceptionDetailId { get; set; }
}