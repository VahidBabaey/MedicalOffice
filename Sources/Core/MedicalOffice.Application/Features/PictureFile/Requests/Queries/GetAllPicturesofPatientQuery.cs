using MediatR;
using MedicalOffice.Application.Dtos.PictureDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PictureFile.Requests.Queries
{
    public class GetAllPicturesofPatientQuery : IRequest<List<PatientPicturesDTO>>
    {
        public Guid PatientId { get; set; }
    }
}
