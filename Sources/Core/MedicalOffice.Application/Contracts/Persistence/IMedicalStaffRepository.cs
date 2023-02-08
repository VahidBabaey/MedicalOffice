using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMedicalStaffRepository : IGenericRepository<MedicalStaff, Guid>
    {
        Task DeleteUserOfficeRoleAsync(Guid UserId);

        Task<List<MedicalStaffListDTO>> GetAllMedicalStaffs(Guid officeID);

        Task<bool> CheckExistByOfficeIdAndPhoneNumber(Guid officeId, string phoneNumber);

        Task UpdateUserOfficeRoleAsync(Guid roleId, Guid UserId);

        Task<IEnumerable<MedicalStaffNameListDTO>> GetAllMedicalStaffsName();

        Task<List<MedicalStaffNamesDTO>> GetAllMedicalStaffsNamesandRoles(Guid officeId);

        Task<bool> CheckMedicalStaffExist(Guid MedicalStaffId, Guid officeId);

        Task<List<MedicalStaff>> GetMedicalStaffBySearch(string name, Guid officeId);
    }
}
