using MediatR;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientReferralFormFile.Request.Query
{
    public class GetAlliillnessReasonsForReferalFormQuery : IRequest<List<illnessNamesListDTO>>
    {
    }
}
