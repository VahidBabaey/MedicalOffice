using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPatientRepository : IGenericRepository<Patient, Guid>
    {
        Task<bool> CheckExistByNationalId(string? nationalId, Guid officeId, Guid? id = null);
        Task<bool> CheckExistInsuranceId(Guid officeId, Guid insuranceId);
        Task<bool> CheckExistIntroducerId(Guid officeId, Guid introducerId);
        Task<bool> CheckExistPatientId(Guid officeId, Guid patientId);
        Task<int> GenerateFileNumber();
        Task<List<PatientListDTO>> GetAllPateint(Guid officeId);
        Task<PatientAddress> InsertAddressofPatientAsync(Guid patientid, string address);
        Task<PatientContact> InsertContactValueOfPatientAsync(Guid patientid, string contactnumber, ContactType type);
        Task<PatientTag> InsertTagofPatientAsync(Guid patientid, string tag);
        Task<bool> IsFileNumberExist(int? fileNumber, Guid officeId, Guid? id = null);
        Task<bool> RemovePatientAddress(Guid patientId);
        Task<bool> RemovePatientContact(Guid patientId);
        Task<bool> RemovePatientTag(Guid patientId);
        Task<List<PatientListDTO>> SearchPateint(Guid officeId, string firstName, string lastName, string nationalCode, string phoneNumber, int fileNumber);
    }
}
