using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Commons;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.ReceptionDTO;

public class ReceptionDetailDTO : IInsuranceIdDTO, IServiceIdDTO
{
    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid? ReceptionId { get; set; }
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
    public Guid? InsuranceId { get; set; }
    /// <summary>
    /// آیدی بیمه تکمیلی
    /// </summary>
    public Guid AdditionalInsuranceId { get; set; }
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
    public long Debt { get; set; }
    /// <summary>
    /// جمع کل
    /// </summary>
    public float Total { get; } //readonly
    /// <summary>
    /// تایپ پذیرش : پرداخت مبلغ امانی - پرداخت بدهی - برگشتی - پکیج - بدون فرانشیز - عادی
    /// </summary>
    public ReceptionType ReceptionType { get; set; }
    /// <summary>
    /// آیدی بیمار
    /// </summary>
    public Guid PatientId { get; set; }
    /// <summary>
    /// آیدی شیفت
    /// </summary>
    public Guid ShiftId { get; set; }
    /// <summary>
    /// آیدی یوزر لاگین شده
    /// </summary>
    public Guid MedicalStaffId { get; set; }
    /// <summary>
    /// آیدی تیم پزشکی
    /// </summary>
    public Guid[] MedicalStaffs { get; set; }
}