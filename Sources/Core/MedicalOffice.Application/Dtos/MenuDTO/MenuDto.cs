using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Dtos.MenuDTO
{
    public class MenuDto
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public string PersianName { get; set; }

        public string? Link { get; set; }

        public List<MenuDto> children { get; set; }=new List<MenuDto>();
    }

}