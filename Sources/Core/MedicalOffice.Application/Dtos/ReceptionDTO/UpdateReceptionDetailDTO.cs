using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Commons;
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
    /// جمع کل
    /// </summary>
    public long Costd { get; set; }
    /// <summary>
    /// مبلغ تخفیف
    /// </summary>
    public float? Discount { get; set; }
    /// <summary>
    /// آیدی تیم پزشکی
    /// </summary>
    public Guid[] MedicalStaffs { get; set; }
}