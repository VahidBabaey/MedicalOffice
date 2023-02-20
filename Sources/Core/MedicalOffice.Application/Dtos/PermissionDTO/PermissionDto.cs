namespace MedicalOffice.Application.Dtos.PermissionDTO
{
    public class PermissionDto
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public bool IsPermitted { get; set; }

        public List<PermissionDto> Children { get; set; }
    }
}