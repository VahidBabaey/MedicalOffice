using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.Experiment.Requests.Commands
{
    public class DeleteExperimentCommand : IRequest<BaseResponse>
    {
        public Guid ExperimentID { get; set; }
        public Guid OfficeId { get; set; }
    }
}
