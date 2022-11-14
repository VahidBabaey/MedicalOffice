using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMedicalStaffRepository : IGenericRepository<MedicalStaff, Guid>
    {
        Task DeleteUserOfficeRoleAsync(Guid MedicalStaffId);
        Task<IEnumerable<MedicalStaffListDTO>> GetAllMedicalStaffs();
        Task<IEnumerable<MedicalStaffNameListDTO>> GetAllMedicalStaffsName();
        Task<List<MedicalStaffNamesDTO>> GetAllMedicalStaffsNamesandRoles();
        Task UpdateUserOfficeRoleAsync(Guid roleId, Guid MedicalStaffId);
    }
}
