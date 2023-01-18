using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.Experiment.Requests.Queries
{
    public class GetExperimentBySearchQuery : IRequest<BaseResponse>
    {
        public string Name { get; set; }
        public Guid OfficeId { get; set; }
    }
}
