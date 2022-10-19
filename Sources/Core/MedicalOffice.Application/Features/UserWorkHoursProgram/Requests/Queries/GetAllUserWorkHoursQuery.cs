using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.UserWorkHoursProgramFileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.UserWorkHoursProgram.Requests.Queries
{
    public class GetAllUserWorkHoursQuery : IRequest<List<UserWorkHoursProgramListDTO>>
    {
        public Guid UserId { get; set; }
    }
}
