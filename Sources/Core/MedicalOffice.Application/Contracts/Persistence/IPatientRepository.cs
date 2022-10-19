using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPatientRepository : IGenericRepository<Patient, Guid>
    {
        Task<Patient?> GetByPhoneNumber(string phoneNumber);
        Task<PatientAddress> InsertAddressofPatientAsync(Guid patientid, string address);
        Task<PatientContact> InsertContactValueofPatientAsync(Guid patientid, string contactnumber);
        Task<PatientTag> InsertTagofPatientAsync(Guid patientid, string tag);
        Task<IReadOnlyList<PatientListDto>> SearchPateint(string nationalCode, string phoneNumber, string fileNumber, string fullname);
        //Task<List<Patient>> GetAllUsingDynamicSearch(PatientSearchDto filters);

    }
}
