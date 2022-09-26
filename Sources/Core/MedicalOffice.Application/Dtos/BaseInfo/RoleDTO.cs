using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.BaseInfo;
public class RoleDTO : BaseDto<Guid>
{
    public string Title { get; set; }

    public RoleDTO(string title = "")
    {
        Title = title;
    }
}
