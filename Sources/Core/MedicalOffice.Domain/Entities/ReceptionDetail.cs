using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// جزئیات پذیرش
/// </summary>
public class ReceptionDetail : BaseDomainEntity<Guid>
{
    /// <summary>
    /// پذیرش
    /// </summary>
    public Reception? Reception { get; set; }
    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid? ReceptionId { get; set; }
    /// <summary>
    /// مطب
    /// </summary>
    public Office? Office { get; set; }
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// خدمت
    /// </summary>
    public Service Service { get; set; } = new();
    /// <summary>
    /// آیدی خدمت
    /// </summary>
    public Guid ServiceId { get; set; }
    /// <summary>
    /// تعداد
    /// </summary>
    public float ServiceCount { get; set; }
    /// <summary>
    /// بیمه - بیمه تکمیلی
    /// </summary>
    public Insurance? Insurance { get; set; }
    /// <summary>
    /// آیدی بیمه
    /// </summary>
    public Guid InsuranceId { get; set; }
    /// <summary>
    /// آیدی بیمه تکمیلی
    /// </summary>
    public Guid AdditionalInsuranceId { get; set; }
    /// <summary>
    /// کاربران / پزشکان
    /// </summary>
    public ICollection<ReceptionMedicalStaff> ReceptionMedicalStaffs { get; set; } = new List<ReceptionMedicalStaff>();
    /// <summary>
    /// هزینه دریافتی
    /// </summary>
    public long Cost { get; set; }
    /// <summary>
    /// اطلاعات تخفیف ها
    /// </summary>
    public ReceptionDiscount? ReceptionDiscount { get; set; }
    /// <summary>
    /// آیدی تخفیف
    /// </summary>
    public Guid ReceptionDiscountId { get; set; }
    /// <summary>
    /// مبلغ امانی / بیعانه
    /// </summary>
    public long Deposit { get; set; }
    /// <summary>
    /// بدهی
    /// </summary>
    public long Debt { get; set; }
    /// <summary>
    /// جمع کل
    /// </summary>
    public long Received { get; set; }
}