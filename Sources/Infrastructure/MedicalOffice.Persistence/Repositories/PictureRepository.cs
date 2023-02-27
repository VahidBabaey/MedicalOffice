using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.PictureDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class PictureRepository : GenericRepository<Picture, Guid>, IPictureRepository
{
    private readonly IGenericRepository<Picture, Guid> _pictureRepository;
    private readonly ApplicationDbContext _dbContext;
    public List<PatientPicturesDTO> PatientPicturesDTO = null;

    public PictureRepository(IGenericRepository<Picture, Guid> pictureRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _pictureRepository = pictureRepository;
        PatientPicturesDTO = new List<PatientPicturesDTO>();
    }
    public async Task<Picture> RegisterPictureAsync(PictureUploadDTO pictureUploadDTO, Guid officeId)
    {
        try
        {
            var picture = new Picture();
            picture.PictureName = pictureUploadDTO.Picturename;
            picture.OfficeId = officeId;
            picture.PatientId = pictureUploadDTO.PatientId;
            var fileName = "" + picture.PictureName;
            byte[] pictureBinary = null;
            using (var fileStream = pictureUploadDTO.File.OpenReadStream())
            {
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    pictureBinary = ms.ToArray();
                }
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            await File.WriteAllBytesAsync(filePath, pictureBinary);

            picture.VirtualPath = filePath;

            await _pictureRepository.Add(picture);

            AddPictureDTO pictureDTO = new AddPictureDTO()
            {
                VirtualPath = picture.VirtualPath,
                Picturename = picture.PictureName
            };

            return picture;
        }
        catch (Exception)
        {
            throw;
        }
        
    }
    public async Task<List<PatientPicturesDTO>> GetByPatientId(Guid patientId)
    {
        var patientpicture = await _dbContext.Pictures.Where(p => p.PatientId == patientId && p.IsDeleted == false).ToListAsync();
        foreach (var item in patientpicture)
        {
            var q = new PatientPicturesDTO()
            {
                PatientId = item.PatientId,
                PictureId = item.Id,
                VirtualPath = item.VirtualPath,
                OfficeId = item.OfficeId,
                Id = item.Id,
                PictureName = item.PictureName,
            };

            PatientPicturesDTO.Add(q);
        }

        return PatientPicturesDTO;
    }
    public async Task<bool> CheckExistPictureId(Guid officeId, Guid pictureId)
    {
        bool isExist = await _dbContext.Pictures.AnyAsync(p => p.OfficeId == officeId && p.Id == pictureId);
        return isExist;
    }
}
