using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.PictureDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class PictureRepository : GenericRepository<Picture, Guid>, IPictureRepository
{
    private readonly ApplicationDbContext _dbContext;
    public List<PatientPicturesDTO> PatientPicturesDTO = null;

    public PictureRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        PatientPicturesDTO = new List<PatientPicturesDTO>();
    }
    public async Task<AddPictureDTO> RegisterPictureAsync(PictureUploadDTO pictureUploadDTO)
    {
        try
        {
            var picture = new Picture();
            picture.PictureName = pictureUploadDTO.Picturename;
            picture.OfficeId = pictureUploadDTO.OfficeId;
            picture.PatientId = pictureUploadDTO.PatientId;

            await _dbContext.Pictures.AddAsync(picture);

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

            _dbContext.Pictures.Update(picture);

            AddPictureDTO pictureDTO = new AddPictureDTO()
            {
                VirtualPath = picture.VirtualPath,
                Picturename = picture.PictureName
            };

            return pictureDTO;
        }
        catch (Exception)
        {
            throw;
        }
        
    }
    public async Task<List<PatientPicturesDTO>> GetByPatientId(Guid patientId)
    {
        var patientpicture = await _dbContext.Pictures.Where(srv => srv.PatientId == patientId).ToListAsync();
        foreach (var item in patientpicture)
        {
            var q = new PatientPicturesDTO()
            {
                PatientId = item.PatientId,
                PictureId = item.Id,
                VirtualPath = item.VirtualPath,
                OfficeId = item.OfficeId
            };

            PatientPicturesDTO.Add(q);
        }

        return PatientPicturesDTO;
    }
}
