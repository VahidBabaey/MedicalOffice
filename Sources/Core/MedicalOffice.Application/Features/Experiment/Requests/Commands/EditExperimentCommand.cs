using MediatR;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.Experiment.Requests.Commands
{
    public class EditExperimentCommand : IRequest<BaseCommandResponse>
    {
        public UpdateExperimentDTO DTO { get; set; } = new UpdateExperimentDTO();
    }
}
