using MediatR;
using MedicalOffice.Application.Dtos.UserWorkHoursProgramFileDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.UserWorkHoursProgram.Requests.Commands
{
    public class EditUserWorkHoursProgramCommand : IRequest<BaseResponse>
    {
        public UserWorkHoursProgramDTO DTO { get; set; } = new UserWorkHoursProgramDTO();
    }
}
