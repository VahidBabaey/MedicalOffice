using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.CashDTO;

public class CashListDTO :BaseDto<Guid>
{
    /// <summary>
    /// آیدی صندوق
    /// </summary>
    public Guid CashId { get; set; }
    /// <summary>
    /// پرداختی
    /// </summary>
    public float Cost { get; set; }
    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid ReceptionId { get; set; }
    /// <summary>
    /// تاریخ پرداخت
    /// </summary>
    public DateTime CreatedDate { get; set; }
    /// <summary>
    /// نوع پرداخت
    /// </summary>
    public CashType? CashType { get; set; }


}
