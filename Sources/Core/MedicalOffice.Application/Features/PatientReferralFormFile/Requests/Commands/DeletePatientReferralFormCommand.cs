using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Commands
{
    public class DeletePatientReferralFormCommand : IRequest<BaseResponse>
    {
        public Guid PatientReferralFormId { get; set; }
    }
}
