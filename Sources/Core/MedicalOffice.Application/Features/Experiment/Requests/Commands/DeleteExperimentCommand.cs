using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.Experiment.Requests.Commands
{
    public class DeleteExperimentCommand : IRequest<BaseCommandResponse>
    {
        public Guid ExperimentID { get; set; }
    }
}
