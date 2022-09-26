using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.Specialization;

public class SpecializationDTO : BaseDto<Guid>
{

    /// <summary>
    /// نام تخصص
    /// </summary>
    public string Title { get; set; } = string.Empty;

}
