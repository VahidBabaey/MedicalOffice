using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Commons;

namespace MedicalOffice.Application.Dtos.ServiceDTO;

public class UpdateServiceDTO : BaseDto<Guid>, ISectionIdDTO
{
    /// <summary>
    /// آیدی بخش
    /// </summary>
    public Guid SectionId { get; set; }
    /// <summary>
    /// آیدی عضویت
    /// </summary>
    public Guid? MembershipId { get; set; }
    /// <summary>
    /// نام خدمت
    /// </summa?ry>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// کد ژنریک
    /// </summary>
    public string GenericCode { get; set; } = string.Empty;
    /// <summary>
    /// آیدی تخصص مربوط به خدمت
    /// </summary>
    public Guid? SpecializationId { get; set; }
    /// <summary>
    /// تعرفه توسط کاربر تعیین گردد یا خیر
    /// </summary>
    public bool HasTariff { get; set; }
    /// <summary>
    /// عملی هست یا نه
    /// </summary>
    public bool IsPractical { get; set; }
    /// <summary>
    /// هزینه مواد مصرفی
    /// </summary>
    public bool IsConsumingMaterials { get; set; }
    /// <summary>
    /// کسر موجودی از انبار
    /// </summary>
    public bool IsDecreasStore { get; set; }
}
