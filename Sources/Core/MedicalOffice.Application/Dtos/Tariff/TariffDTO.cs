using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Commons;

namespace MedicalOffice.Application.Dtos.Tariff;

public class TariffDTO : IServiceIdDTO, IInsuranceIdDTO
{
    /// <summary>
    /// آیدی خدمت
    /// </summary>
    public Guid ServiceId { get; set; }
    /// <summary>
    /// آیدی بیمه
    /// </summary>
    public Guid InsuranceId { get; set; }
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
