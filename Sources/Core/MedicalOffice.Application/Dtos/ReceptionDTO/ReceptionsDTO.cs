using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Domain.Enums;


namespace MedicalOffice.Application.Dtos.ReceptionDTO;

public class ReceptionsDTO : IPatientIdDTO
{
    /// <summary>
    /// تایپ پذیرش : پرداخت مبلغ امانی - پرداخت بدهی - برگشتی - پکیج - بدون فرانشیز - عادی
    /// </summary>
    public ReceptionType ReceptionType { get; set; }
    /// <summary>
    /// آیدی بیمار
    /// </summary>
    public Guid PatientId { get; set; }

}