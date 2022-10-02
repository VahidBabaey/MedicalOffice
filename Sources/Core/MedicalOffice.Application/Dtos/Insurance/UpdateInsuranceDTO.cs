using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.Insurance;

public class UpdateInsuranceDTO : BaseDto<Guid>
{
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

}