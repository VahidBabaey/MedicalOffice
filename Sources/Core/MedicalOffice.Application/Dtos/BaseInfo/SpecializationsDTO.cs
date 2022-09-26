using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.BaseInfo;
public class SpecializationsDTO : BaseDto<Guid>
{
    public string Title { get; set; }

    public SpecializationsDTO(string title = "")
    {
        Title = title;
    }
}
