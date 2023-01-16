using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.Experiment.Requests.Queries
{
    public class GetAllExperimentQuery : IRequest<List<ExperimentListDTO>>
    {
        public ListDto DTO { get; set; } = new ListDto();
        public Guid OfficeId { get; set; }
    }
}
