using MediatR;
using MedicalOffice.Application.Dtos.Patient;
using MedicalOffice.Application.Dtos.PictureDTO;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PictureFile.Requests.Commands
{
    public class AddPictureCommand : IRequest<BaseResponse>
    {
        public PictureUploadDTO DTO { get; set; } = new PictureUploadDTO();
        public PatientPicturesDTO DTOpatientpicture { get; set; } = new PatientPicturesDTO();
    }
}
