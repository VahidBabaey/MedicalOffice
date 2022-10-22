using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// خدمت
/// </summary>
public class Service : BaseDomainEntity<Guid>
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
    /// بخش
    /// </summary>
    public Section? Section { get; set; }
    /// <summary>
    /// نوع خدمت
    /// </summary>
    public ServiceType? ServiceType { get; set; }
    /// <summary>
    /// آیدی بخش
    /// </summary>
    public Guid? SectionId { get; set; }
    /// <summary>
    /// نام خدمت
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// کد ژنریک
    /// </summary>
    public string GenericCode { get; set; } = string.Empty;
    /// <summary>
    /// تخصص مربوط به خدمت
    /// </summary>
    public Specialization? Specialization { get; set; }
    /// <summary>
    /// آیدی تخصص مربوط به خدمت
    /// </summary>
    public Guid? SpecializationId { get; set; }
    /// <summary>
    /// تعرفه توسط کاربر تعیین گردد یا خیر
    /// </summary>
    public bool HasTariff { get; set; }
    /// <summary>
    /// تعرفه های این خدمت
    /// </summary>
    public ICollection<Tariff> Tariffs { get; set; } = new List<Tariff>();
    /// <summary>
    /// عملی هست یا نه
    /// </summary>
    public bool IsPractical { get; set; }
    /// <summary>
    /// هزینه مواد مصرفی
    /// </summary>
    public bool IsConsumingMaterials { get; set; }
    /// <summary>
    /// سهم و درصد کاربران
    /// </summary>
    public ICollection<ServiceSharePercent>? UserServiceSharePercents { get; set; }
    /// <summary>
    /// جزئیات پذیرش
    /// </summary>
    public ICollection<ReceptionDetail>? ReceptionDetails { get; set; }
    /// <summary>
    /// خدمات وقت دهی ها
    /// </summary>
    public ICollection<AppointmentService>? AppointmentServices { get; set; }
    /// <summary>
    /// از این مدل برای برقراری ارتباط یک به چند بین عضویت-سرویس استفاده می شود
    /// </summary>
    public ICollection<MemberShipService>? MemberShipServices { get; set; }

}