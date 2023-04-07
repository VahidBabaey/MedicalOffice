
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.ReceptionDTO;

public class ReceptionDetailSharesDTO
{
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