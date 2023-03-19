using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Dtos.CashDTO;

public class UpdateCashCartDTO : BaseDto<Guid>
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
    /// شماره کارت
    /// </summary>
    public string CartNumber { get; set; }
    /// <summary>
    /// تاریخ
    /// </summary>
    public string Date { get; set; }
    /// <summary>
    /// آیدی بانک
    /// </summary>
    public Guid BankId { get; set; }
    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid ReceptionId { get; set; }

}
