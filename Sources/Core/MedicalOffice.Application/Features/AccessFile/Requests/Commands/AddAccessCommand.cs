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
    public class AddAccessCommand : IRequest<BaseCommandResponse>
    {
        public string userid { get; set; } = string.Empty;
        public AccessDTO DTO { get; set; } = new AccessDTO();
    }
}
