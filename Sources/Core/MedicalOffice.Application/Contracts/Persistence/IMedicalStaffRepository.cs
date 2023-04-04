using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMedicalStaffRepository : IGenericRepository<MedicalStaff, Guid>
    {
        Task<List<MedicalStaffListDTO>> GetAllMedicalStaffs(Guid officeID);

        Task<bool> CheckExistByOfficeIdAndPhoneNumber(Guid officeId, string phoneNumber);

        Task UpdateUserOfficeRoleAsync(Guid roleId, Guid UserId);

        Task<IEnumerable<MedicalStaffNameReferrerListDTO>> GetAllReferrerMedicalStaffsName(Guid officeId);

        Task<List<MedicalStaffNamesDTO>> GetAllMedicalStaffsNamesandRoles(Guid officeId);

        Task<bool> CheckMedicalStaffExist(Guid MedicalStaffId, Guid officeId);

        Task<List<MedicalStaff>> GetMedicalStaffBySearch(string name, Guid officeId);

        Task<List<MedicalStaff>> GetAllDoctorsAndExperts(Guid officeId);

        Task<bool> CheckMedicalStaffReferrerExist(Guid? MedicalStaffId, Guid officeId);

        Task<MedicalStaff> GetExistingStaffById(Guid id, Guid officeId);

        Task<MedicalStaff> GetStaffMedicalSystemInfo(Guid officeId);
    }
}
