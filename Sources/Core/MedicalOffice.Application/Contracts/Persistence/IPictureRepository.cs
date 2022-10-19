using MedicalOffice.Application.Dtos.PictureDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPictureRepository : IGenericRepository<Picture, Guid>
    {
        Task<List<PatientPicturesDTO>> GetByPatientId(Guid patientId);
        Task<AddPictureDTO> RegisterPictureAsync(PictureUploadDTO pictureUploadDTO);
    }
}
