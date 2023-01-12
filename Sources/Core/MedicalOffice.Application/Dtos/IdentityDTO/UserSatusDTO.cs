using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.Identity
{
    public class UserStatusDTO
    {
        public bool IsExist { get; set; } = true;

        public bool HasPassword { get; set; } 

        public bool IsActive{ get; set; } = true;
    }
}