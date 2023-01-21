using MediatR;
using MedicalOffice.Application.Dtos.PictureDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PictureFile.Requests.Queries
{
    public class GetAllPicturesofPatientQuery : IRequest<BaseResponse>
    {
        public Guid PatientId { get; set; }
    }
}
