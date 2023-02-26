namespace MedicalOffice.Application.Dtos.PermissionDTO
{
    public class UpdateStaffPermissionDto
    {
        public Guid staffId { get; set; }

        public List<Guid> permissionIds { get; set; }
    }
}