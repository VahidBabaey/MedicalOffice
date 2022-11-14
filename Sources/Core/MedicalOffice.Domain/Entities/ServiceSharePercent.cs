using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// سهم و درصد کاربران / پزشکان
/// </summary>
public class MedicalStaffServiceSharePercent : BaseDomainEntity<Guid>
{
    /// <summary>
    /// کاربر
    /// </summary>
    public UserOfficeRole? UserOfficeRole { get; set; }
    /// <summary>
    /// آیدی کاربر
    /// </summary>
    public Guid UserOfficeRoleId { get; set; }
    /// <summary>
    /// شیفت
    /// </summary>
    public Shift? Shift { get; set; }
    /// <summary>
    /// آیدی شیفت
    /// </summary>
    public Guid ShiftId { get; set; }
    /// <summary>
    /// خدمت
    /// </summary>
    public Service? Service { get; set; }
    /// <summary>
    /// آیدی خدمت
    /// </summary>
    public Guid ServiceId { get; set; }
    /// <summary>
    /// بخش
    /// </summary>
    public Section? Section { get; set; }
    /// <summary>
    /// آیدی بخش
    /// </summary>
    public Guid SectionId { get; set; }
    /// <summary>
    /// درصد سهم
    /// </summary>
    public float SharePercent { get; set; }
    /// <summary>
    /// مبلغ سهم
    /// </summary>
    public float ShareAmount { get; set; }
    /// <summary>
    /// کاربران مطب
    /// </summary>
    public ICollection<ReceptionMedicalStaff>? ReceptionMedicalStaffs { get; set; }
}