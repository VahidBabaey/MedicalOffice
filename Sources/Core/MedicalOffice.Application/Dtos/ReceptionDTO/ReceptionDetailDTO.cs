using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.ReceptionDTO;

public class ReceptionDetailDTO : IInsuranceIdDTO, IServiceIdDTO
{
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
    public Guid? AdditionalInsuranceId { get; set; }

    /// <summary>
    /// تایپ پذیرش : پرداخت مبلغ امانی - پرداخت بدهی - برگشتی - پکیج - بدون فرانشیز - عادی
    /// </summary>
    public ReceptionType ReceptionType { get; set; }

    /// <summary>
    /// آیدی بیمار
    /// </summary>
    public Guid PatientId { get; set; }

    /// <summary>
    /// آیدی عضویت
    /// </summary>
    public Guid? MembershipId { get; set; }

    /// <summary>
    /// درصد تخفیف
    /// </summary>
    public long Discount { get; set; }

    /// <summary>
    /// آیدی تیم پزشکی
    /// </summary>
    public Guid[]? MedicalStaffs { get; set; }
    /// <summary>
    /// تعرفه سرویس
    /// </summary>
    public long Tariff { get; set; }
    /// <summary>
    /// قابل پرداخت
    /// </summary>
    public long Payable { get; set; }
    /// <summary>
    /// جمع کل
    /// </summary>
    public long Total { get; set; }
    /// <summary>
    /// دریافتی
    /// </summary>
    public long Recieved { get; set; }

    /// <summary>
    /// سهم سازمان
    /// </summary>
    public long OrganShare { get; set; }

    /// <summary>
    /// سهم بیمار
    /// </summary>
    public long PatientShare { get; set; }

    /// <summary>
    /// سهم بیمه تکمیلی
    /// </summary>
    public long AdditionalInsuranceShare { get; set; }

    /// <summary>
    /// شناسه پذیرش
    /// </summary>
    public Guid? ReceptionId { get; set; }
}