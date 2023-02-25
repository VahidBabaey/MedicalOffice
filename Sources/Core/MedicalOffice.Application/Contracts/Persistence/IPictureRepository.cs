using MedicalOffice.Application.Dtos.PictureDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPictureRepository : IGenericRepository<Picture, Guid>
    {
        Task<bool> CheckExistPictureId(Guid officeId, Guid pictureId);
        Task<List<PatientPicturesDTO>> GetByPatientId(Guid patientId);
        Task<Picture> RegisterPictureAsync(PictureUploadDTO pictureUploadDTO, Guid officeId);
    }
}
