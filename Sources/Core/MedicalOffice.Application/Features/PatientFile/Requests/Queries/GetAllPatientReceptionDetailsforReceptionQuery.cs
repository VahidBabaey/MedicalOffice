using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Queries
{
    public class GetAllPatientReceptionDetailsforReceptionQuery : IRequest<BaseResponse> 
    {
        public ReceptionDetailListForReceptionDTO DTO { get; set; } = new ReceptionDetailListForReceptionDTO();
        public Guid PatientId { get; set; }
        public Guid ReceptionId { get; set; }
        public Guid OfficeId { get; set; }
    }
}
