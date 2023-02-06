using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPatientRepository : IGenericRepository<Patient, Guid>
    {
        Task<bool> CheckExistInsuranceId(Guid officeId, Guid insuranceId);
        Task<bool> CheckExistIntroducerId(Guid officeId, Guid introducerId);
        Task<bool> CheckExistPatientId(Guid officeId, Guid patientId);
        int GenerateFileNumber();
        Task<PatientAddress> InsertAddressofPatientAsync(Guid patientid, string address);
        Task<PatientContact> InsertContactValueofPatientAsync(Guid patientid, string contactnumber);
        Task<PatientTag> InsertTagofPatientAsync(Guid patientid, string tag);
        Task<bool> RemovePatientAddress(Guid patientId);
        Task<bool> RemovePatientContact(Guid patientId);
        Task<bool> RemovePatientTag(Guid patientId);
        Task<List<PatientListDTO>> SearchPateint(string firstName, string lastName, string nationalCode, string phoneNumber, string fileNumber);
    }
}
