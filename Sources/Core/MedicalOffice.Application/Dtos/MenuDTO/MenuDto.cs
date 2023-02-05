using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Dtos.MenuDTO
{
    public class MenuDto
    {
        public Guid MenuId { get; set; }

        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public string? Link { get; set; }

        public List<Menu> children { get; set; }
    }
}