using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// تعرفه
/// </summary>
public class Tariff : BaseDomainEntity<Guid>
{
    /// <summary>
    /// مرکز درمانی - مطب
    /// </summary>
    public Office Office { get; set; }

    /// <summary>
    /// آیدی مرکز درمانی - مطب
    /// </summary>
    public Guid OfficeId { get; set; }

    /// <summary>
    /// خدمات
    /// </summary>
    public Service Service { get; set; }

    /// <summary>
    /// آیدی خدمت
    /// </summary>
    public Guid ServiceId { get; set; }

    /// <summary>
    /// نوع سرویس
    /// </summary>
    public ServiceType ServiceType { get; set; }

    /// <summary>
    /// تعرفه قابل تعیین توسط کاربر است یا خیر
    /// </summary>
    public Insurance? Insurance { get; set; }

    /// <summary>
    /// آیدی بیمه
    /// </summary>
    public Guid? InsuranceId { get; set; }

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
    public int Discount { get; set; }

    /// <summary>
    /// درصد بیمه
    /// </summary>
    public int InsurancePercent { get; set; }
}