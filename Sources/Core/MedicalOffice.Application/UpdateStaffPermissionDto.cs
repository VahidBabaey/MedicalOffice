namespace MedicalOffice.Application
{
    public class UpdateStaffPermissionDto
    {
        public Guid staffId { get; set; }

        public List<Guid> permissionIds { get; set; }
    }
}