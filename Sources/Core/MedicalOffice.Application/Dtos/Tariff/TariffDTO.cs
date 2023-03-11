using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Commons;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.Tariff;

public class TariffDTO : IServiceIdDTO
{
    /// <summary>
    /// آیدی خدمت
    /// </summary>
    public Guid ServiceId { get; set; }

    /// <summary>
    /// لیست تعرفه ها
    /// </summary>
    public List<TariffList> Tariffs { get; set; }
}

public class TariffList : IInsuranceIdDTO
{
    /// <summary>
    /// آیدی بیمه
    /// </summary>
    public Guid? InsuranceId { get; set; }

    /// <summary>
    /// کد بیمه
    /// </summary>
    public long? InsuranceCode { get; set; }

    /// <summary>
    /// مبلغ تعرفه
    /// </summary>
    public float TariffValue { get; set; }

    /// <summary>
    /// تعرفه داخلی
    /// </summary>
    public float InternalTariffValue { get; set; }

    /// <summary>
    /// نوع سرویس
    /// </summary>
    public ServiceType ServiceType { get; set; }

    /// <summary>
    /// ما به التفاوت
    /// </summary>
    public float? Difference { get; set; }

    /// <summary>
    /// تخفیف
    /// </summary>
    public int Discount { get; set; }

    /// <summary>
    /// درصد بیمه
    /// </summary>
    public int InsurancePercent { get; set; }
}

