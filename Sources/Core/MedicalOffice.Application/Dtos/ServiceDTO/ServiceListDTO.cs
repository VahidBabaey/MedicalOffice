using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.ServiceDTO;

public class ServiceListDTO : BaseDto<Guid>
{
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }

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
    /// آیدی تخصص مربوط به خدمت
    /// </summary>
    public Guid? SpecializationId { get; set; }

}
