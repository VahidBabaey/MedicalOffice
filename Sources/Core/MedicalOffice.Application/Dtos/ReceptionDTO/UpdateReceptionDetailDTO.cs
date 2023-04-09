using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.ReceptionDTO;

public class UpdateReceptionDetailDTO : IInsuranceIdDTO, IServiceIdDTO, IReceptionIdDTO
{
    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid ReceptionId { get; set; }
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
    /// مبلغ تخفیف
    /// </summary>
    public long Discount { get; set; }
    /// <summary>
    /// آیدی تیم پزشکی
    /// </summary>
    public Guid[]? MedicalStaffs { get; set; }
    /// تعرفه سرویس
    /// </summary>
    public long Tariff { get; set; }
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
}