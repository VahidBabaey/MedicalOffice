namespace MedicalOffice.Application.Dtos.PermissionDTO
{
    public class PermissionListDto
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public string PersianName { get; set; }

        public bool IsShown { get; set; }

        public List<PermissionListDto> Children { get; set; }
    }
}