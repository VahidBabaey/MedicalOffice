using MediatR;
using MedicalOffice.Application.Dtos.AccessDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AccessFile.Requests.Commands
{
    public class EditAccessCommand : IRequest<BaseCommandResponse>
    {
        public UpdateAccessDTO DTOUp { get; set; } = new UpdateAccessDTO();
    }
}
