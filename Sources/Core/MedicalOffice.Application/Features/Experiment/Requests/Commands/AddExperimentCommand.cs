using MediatR;
using MedicalOffice.Application.Dtos.Experiment;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.Experiment.Requests.Commands
{
    public class AddExperimentCommand : IRequest<BaseCommandResponse>
    {
      public ExperimentDTO DTO { get; set; } = new ExperimentDTO();
    }
}
