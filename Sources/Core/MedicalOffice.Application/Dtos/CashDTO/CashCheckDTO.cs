
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Dtos.CashDTO;

public class CashCheckDTO 
{
    /// <summary>
    /// پرداختی
    /// </summary>
    public long Cost { get; set; }

    /// <summary>
    /// شماره حساب
    /// </summary>
    public string AccountNumber { get; set; }

    /// <summary>
    /// آیدی بانک
    /// </summary>
    public Guid BankId { get; set; }

    /// <summary>
    /// شعبه
    /// </summary>
    public string Branch { get; set; }

    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid ReceptionId { get; set; }
    /// <summary>
    /// بدهی
    /// </summary>
    public long TotalDebt { get; set; }

}
