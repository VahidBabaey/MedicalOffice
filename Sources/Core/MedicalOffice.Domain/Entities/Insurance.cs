using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// بیمه
/// </summary>
public class Insurance : BaseDomainEntity<Guid>
{
    /// <summary>
    /// مطب
    /// </summary>
    public Office? Office { get; set; }
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// نام بیمه : تامین - مسلح - غیره
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// کد بیمه
    /// </summary>
    public long InsuranceCode { get; set; }
    /// <summary>
    /// درصد بیمه
    /// </summary>
    public float InsurancePercent { get; set; }
    /// <summary>
    ///  بیمه تکمیلی هست یا نه
    /// </summary>
    public bool IsAdditionalInsurance { get; set; }
    /// <summary>
    /// دارای بیمه تکمیلی هست یا نه
    /// </summary>
    public bool HasAdditionalInsurance { get; set; }
    /// <summary>
    /// نمایش در دیسکت
    /// </summary>
    public bool ShowonDisket { get; set; }
    /// <summary>
    /// عضویت پذیر
    /// </summary>
    public bool Joinable { get; set; }
    /// <summary>
    /// جزئیات پذیرش - بیمه/بیمه تکمیلی
    /// </summary>
    public ICollection<ReceptionDetail>? ReceptionDetails_Insurance { get; set; }
    /// <summary>
    /// تعرفه ها
    /// </summary>
    public ICollection<Tariff>? Tariffs { get; set; }

}