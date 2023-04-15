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
    public class GetAllPatientReceptionsQuery : IRequest<BaseResponse> 
    {
        public ListDto listDTO { get; set; }
        public ReceptionListDTO DTO { get; set; } = new ReceptionListDTO();
        public Guid PatientId { get; set; }
    }
}
