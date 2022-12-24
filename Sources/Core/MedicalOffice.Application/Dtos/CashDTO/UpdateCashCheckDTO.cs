using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Dtos.CashDTO;

public class UpdateCashCheckDTO : BaseDto<Guid>
{
    /// <summary>
    /// آیدی صندوق
    /// </summary>
    public Guid CashId { get; set; }
    /// <summary>
    /// پرداختی
    /// </summary>
    public long Cost { get; set; }
    /// <summary>
    /// شماره حساب
    /// </summary>
    public string AccountNumber { get; set; }
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
