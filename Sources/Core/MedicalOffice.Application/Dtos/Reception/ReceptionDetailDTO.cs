using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Entities;


namespace MedicalOffice.Application.Dtos.Reception;

public class ReceptionDetailDTO 
{

    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid ReceptionId { get; set; }
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// آیدی خدمت
    /// </summary>
    public Guid ServiceId { get; set; }
    /// <summary>
    /// تعداد
    /// </summary>
    public int ServiceCount { get; set; }
    /// <summary>
    /// آیدی بیمه
    /// </summary>
    public Guid InsuranceId { get; set; }
    /// <summary>
    /// آیدی بیمه تکمیلی
    /// </summary>
    public Guid AdditionalInsuranceId { get; set; }
    /// <summary>
    /// هزینه دریافتی
    /// </summary>
    public long Cost { get; set; }
    /// <summary>
    /// آیدی تخفیف
    /// </summary>
    public Guid ReceptionDiscountId { get; set; }
    /// <summary>
    /// مبلغ امانی / بیعانه
    /// </summary>
    public float Deposit { get; set; }
    /// <summary>
    /// بدهی
    /// </summary>
    public float Debt { get; set; }
    /// <summary>
    /// جمع کل
    /// </summary>
    public float Total { get; } //readonly
    /// <summary>
    /// آیدی تیم پزشکی
    /// </summary>
    public Guid[] MedicalStaffs { get; set; }
}