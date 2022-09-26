using MedicalOffice.Application.Dtos.MedicalStaffdto;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMedicalStaffRepository : IGenericRepository<MedicalStaff, Guid>
    {
        Task DeleteUserOfficeRoleAsync(Guid medicalstaffId);
        Task<IEnumerable<MedicalStaffListDTO>> GetAllMedicalStaffs();
        Task<IEnumerable<MedicalStaffNameListDTO>> GetAllMedicalStaffsName();
        Task<UserOfficeRole> InserttoUserOfficeRoleAsync(Guid roleId, Guid medicalstaffId);
        Task UpdateUserOfficeRoleAsync(Guid roleId, Guid medicalstaffId);
    }
}
