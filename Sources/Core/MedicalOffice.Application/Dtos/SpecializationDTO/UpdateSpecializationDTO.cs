using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.SpecializationDTO;

public class UpdateSpecializationDTO : BaseDto<Guid>
{

    /// <summary>
    /// نام تخصص
    /// </summary>
    public string Title { get; set; } = string.Empty;

}
