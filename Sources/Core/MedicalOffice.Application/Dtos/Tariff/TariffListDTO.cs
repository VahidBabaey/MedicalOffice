using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.Tariff;

public class TariffListDTO : BaseDto<Guid>
{

    /// <summary>
    /// آیدی مرکز درمانی - مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// آیدی تعرفه
    /// </summary>
    public Guid TariffId { get; set; }
    /// <summary>
    /// آیدی بیمه
    /// </summary>
    public Guid InsuranceId { get; set; }
    /// <summary>
    /// نام بیمه
    /// </summary>
    public string InsuranceName { get; set; } = string.Empty;
    /// <summary>
    /// آیدی ضرایب کا
    /// </summary>
    public Guid KMultiplierId { get; set; }
    /// <summary>
    /// مبلغ تعرفه
    /// </summary>
    public float TariffValue { get; set; }
    /// <summary>
    /// تعرفه داخلی
    /// </summary>
    public float InternalTariffValue { get; set; }
    /// <summary>
    /// ما به التفاوت
    /// </summary>
    public float Difference { get; set; }
    /// <summary>
    /// تخفیف
    /// </summary>
    public float Discount { get; set; }
    /// <summary>
    /// درصد بیمه
    /// </summary>
    public float InsurancePercent { get; set; }
    /// <summary>
    /// مبلغ اضافه به بیمه
    /// </summary>
    public float AdjunctPrice { get; set; }
}
