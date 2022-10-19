using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.SpecializationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.SpecializationFile.Requests.Queries
{
    public class GetAllSpecializationsQuery : IRequest<List<SpecializationListDTO>>
    {
        public ListDto DTO { get; set; } = new ListDto();
       
    }
}
