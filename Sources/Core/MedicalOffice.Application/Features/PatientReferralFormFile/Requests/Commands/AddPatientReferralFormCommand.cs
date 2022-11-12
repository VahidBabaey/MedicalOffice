using MediatR;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Commands
{
    public class AddPatientReferralFormCommand : IRequest<BaseResponse>
    {
        public PatientReferralFormDTO DTO { get; set; } = new PatientReferralFormDTO();
    }
}
